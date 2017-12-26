using System;
using UnityEngine;

namespace BTS {
	
	public class BTS_InviteController : MonoBehaviour {

		private Animator _animator;

		private const string ANIMATOR_FADE_IN_TRIGGER = "InviteFadeIn";
		private const string ANIMATOR_FADE_OUT_TRIGGER = "InviteFadeOut";
		
		private const string mobileNumber = "";
		
		private string emailSubject = "Bee The Swarm";
		private string messageBody = "Help me raise money for good causes by playing FREE video games www.beetheswarm.com/signup Use code XXXXX and get me extra money for signing you up!";

		private string message = "";
		private string refCode = "";
		
		private void Awake() {
			DontDestroyOnLoad (gameObject);
			_animator = GetComponent<Animator>();
		}

		private void RefreshCode() {
			refCode = BTS_PlayerData.Instance.Player.RefCode;
			message = messageBody.Replace("XXXXX", refCode);
			
			Debug.Log(message);
		}

		public void Show() {
			_animator.SetTrigger(ANIMATOR_FADE_IN_TRIGGER);
		}

		public void Hide() {
			_animator.SetTrigger(ANIMATOR_FADE_OUT_TRIGGER);
		}

		public void SendEmail() {
			
			RefreshCode();
			
			#if UNITY_ANDROID
			Application.OpenURL("mailto:" + "" + "?subject=" + emailSubject + "&body=" + message);
			
			#elif UNITY_IOS
			string subject = EscapeURL(emailSubject);
			string body = EscapeURL(message);
			Application.OpenURL ("mailto:" + "" + "?subject=" + subject + "&body=" + body);
			#endif

		}
		
		 public static string EscapeURL(string url) {
                return WWW.EscapeURL(url).Replace("+", "%20");
         }

		public void SendSMS() {

			RefreshCode();
			
			string URL = String.Empty;
			
			#if UNITY_ANDROID
			URL = string.Format("sms:{0}?body={1}",mobileNumber,System.Uri.EscapeDataString(message));
 
			#elif UNITY_IOS
            URL ="sms:"+mobileNumber+"?&body="+ System.Uri.EscapeDataString(message); //Method4 - Works perfectly
      		#endif
 
			Application.OpenURL(URL);
		}
	}
}