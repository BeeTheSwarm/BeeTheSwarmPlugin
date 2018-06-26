using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
namespace BTS {

    internal class NetworkService : BaseService, INetworkService {
        public const string SECRET = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDzooiCyBcgbuNFeVCCF0/5dma";

        private const string WEBSERVER_VERSION = "1.0";

        public const string WEBSERVER_LOG_HEADER = "ST_WebServer Log: ";
        private const string WEBSERVER_ERROR_HEADER = "ST_WebServer Error: ";
        private const long WEBSERVER_ALLOWED_REQUEST_TIME_RANGE = 900000;

        private string m_requestUrl = string.Empty;
        private string m_gameID;
        private int m_platform;
        [Inject]
        private IUserProfileModel m_userProfile;
        [Inject]
        private IPopupsModel m_popupsModel;
        [Inject]
        private ILocalDataModel m_localDataModel;
        private IContext m_context;
        private string m_authToken = string.Empty;

        public string AuthToken {
            get {
                return m_authToken;
            }
            set {
                m_authToken = value;
                m_localDataModel.SaveToken(value);
            }
        }

        public NetworkService(IContext context) {
            m_context = context;
            m_requestUrl = "https://" + BTS_Settings.GetServerUrl();
            switch (Application.platform) {
                case RuntimePlatform.Android:
                    m_platform = 2;
                    break;
                default:
                    m_platform = 1;
                    break;
            }
        }

        private List<IBasePackage> m_packageQueue = new List<IBasePackage>();
        private IBasePackage m_currentPack;
        private Coroutine m_internetStatusCheck;
        public event Action<NetworkState> OnStateChanged = delegate { };

        public void SendPackage(IBasePackage pack) {
            if (pack.AuthenticationRequired) {
                m_packageQueue.Add(pack);
            }
            else {
                m_packageQueue.Insert(0, pack);
            }
            SendNext();
        }

        private void SendNext() {
            if (m_currentPack != null) {
                return;
            }
            if (m_packageQueue.Count == 0) {
                return;
            }
            m_currentPack = m_packageQueue[0];
            m_context.StartCoroutine(SendRequest(m_currentPack));
        }

        private string GetData(IBasePackage package) {
            var OriginalJSON = new Dictionary<string, object>();
            OriginalJSON.Add("method", package.Id);
            OriginalJSON.Add("fields", package.GenerateData());
            var data = Json.Serialize(OriginalJSON);
            return data;
        }

        private Dictionary<string, string> GetHeaders(IBasePackage package, string data) {
            var hash = HMAC(SECRET, data);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("secret", hash);
            headers.Add("gid", m_gameID);
            headers.Add("platform", m_platform.ToString());
            return headers;
        }

        private IEnumerator SendRequest(IBasePackage package) {
            OnStateChanged.Invoke(NetworkState.Processing);
            if (Application.internetReachability == NetworkReachability.NotReachable) {
                m_popupsModel.AddPopup(new ErrorPopupItemModel("Network unreachable. Check you network connection"));
            }
            while (Application.internetReachability == NetworkReachability.NotReachable) {
                yield return new WaitForSecondsRealtime(5f);
            }
            string data = GetData(package);
            Dictionary<string, string> headers = GetHeaders(package, data);
            string authToken = "";
            if (package.AuthenticationRequired && string.IsNullOrEmpty(AuthToken)) {
                Debug.LogError("auth need for pack " + package.Id);
                m_packageQueue.Remove(package);
                m_currentPack = null;
                SendNext();
                yield break;
            }
            else {
                authToken = AuthToken ?? string.Empty;
            }
            headers.Add("auth", authToken);
            Debug.Log(WEBSERVER_LOG_HEADER + " Send " + data);
            var payload = ASCIIEncoding.UTF8.GetBytes(data);
            var www = new WWW(m_requestUrl, payload, headers);
            yield return www;
            var result = new WS_RequestResult();
            result.rawResponce = www;
            result.Request = package;
            if (www.error == null) {
                HandleRequestCompleted(result);
            }
            else {
                result.Status = BTS_RequestStatus.Failed;
            }
            OnStateChanged.Invoke(NetworkState.Idle);
        }

        void HandleRequestCompleted(WS_RequestResult result) {
            bool isResponceHashValid = ValidateResponceHash(result.rawResponce);
            if (!isResponceHashValid) {
                ValidationFailed(result);
            }
            else {
                Debug.Log(WEBSERVER_LOG_HEADER + "Received Package " + result.Request.Id + " Data: " + result.rawResponce.text);
                result.Request.ParseResponse(result.rawResponce.text);
                if (result.Request.Error.Status) {
                    if (!HandleError(result.Request.Error)) {
                        result.Request.HandleResponce();
                        if (m_packageQueue.Contains(m_currentPack)) {
                            m_packageQueue.Remove(m_currentPack);
                        }
                    }
                }
                else {
                    try {
                        result.Request.HandleResponce();
                    }
                    catch (Exception e) {
                        Debug.LogError("Package " + result.Request.Id + " failed to handle response");
                        Debug.LogError(e.Message + " " + e.StackTrace);
                    }
                    m_packageQueue.Remove(m_currentPack);
                }
            }
            m_currentPack = null;
            SendNext();
        }

        private static bool ValidateResponceHash(WWW rawResponce) {
            var responceHash = rawResponce.responseHeaders["SECRET"];
            var clientHash = HMAC(SECRET, rawResponce.text);
            if (!clientHash.Equals(responceHash)) {
                Debug.Log(WEBSERVER_ERROR_HEADER + " Hash Validation FAILED.");
                return false;
            } 
            Debug.Log("Hash validation ok");
            return true;
        }

        private bool HandleError(BTS_Error error) {
            var handled = true;
            switch (error.Code) {
                case ApiErrors.USER_IS_UNCONFIRMED:
                    m_userProfile.State = UserState.Unconfirmed;
                    m_packageQueue.Clear();
                    handled = false;
                    break;
                case ApiErrors.AUTH_TOKEN_EXPIRED:
                    m_context.CreateCommand<RenewAuthTokenCommand>().Execute();
                    break;
                case ApiErrors.SYSTEM_ERROR:
                case ApiErrors.INVALID_PACKAGE_NAME:
                    m_packageQueue.Remove(m_currentPack);
                    break;
                default:
                    handled = false;
                    break;
            }
            return handled;
        }

        private static void ValidationFailed(WS_RequestResult result) {
        }

        private static string HMAC(string key, string data) {
            var keyByte = ASCIIEncoding.UTF8.GetBytes(key);
            var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte);
            hmacsha256.ComputeHash(ASCIIEncoding.UTF8.GetBytes(data));
            return ByteToString(hmacsha256.Hash);
        }

        private static string ByteToString(byte[] buff) {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++) {
                sbinary += buff[i].ToString("X2");
            }
            return sbinary.ToLower();
        }

        public void SetGameId(string gameId) {
            m_gameID = gameId;
        }

        public void SignOut() {
            m_authToken = string.Empty;
            m_packageQueue.Clear();
        }
    }
}
