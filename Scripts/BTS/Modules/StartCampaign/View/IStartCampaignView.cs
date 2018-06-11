using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BTS {
    internal interface IStartCampaignView : IControlledView {
        void SetViewModel(IStartCampaignViewModel m_viewModel);
        void ShowError(string v);
        void Clear();
        void ShowPostView();
    }
}