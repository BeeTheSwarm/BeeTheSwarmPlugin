using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class LoaderController : BasePopupController<ILoaderView>, ILoaderController {
        [Inject]
        private INetworkService m_networkService;

        public void Show(string message) {
            m_view.SetText(message);
            m_view.Show();
        }
    }
}
