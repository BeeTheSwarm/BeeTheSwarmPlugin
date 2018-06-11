using UnityEngine;
using System.Collections;
using System;
namespace BTS {
    public abstract class BaseNetworkService<T> : BaseService, IServerResponseHandler<T> where T: PackageResponse {
        [Inject]
        protected INetworkService m_networkService;
        [Inject]
        protected IPopupsModel m_errorModel;

        public void OnSuccess(T data) {
            HandleSuccessResponse(data);
            FireSuccessFinishEvent();
        }

        protected abstract void HandleSuccessResponse(T data);
        
        public virtual void OnError(BTS_Error error) {
            m_errorModel.AddPopup(new ErrorPopupItemModel(error.Description));
        }

        public virtual void OnTimeout() {
        }

        protected virtual void SendPackage(BTS_BasePackage<T> package) {
            package.Handler = this;
            m_networkService.SendPackage(package);
        }        
    }
}
