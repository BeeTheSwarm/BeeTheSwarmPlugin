using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public interface IUpdateCampaignView : IControlledView {
        void SetViewModel(IUpdateCampaignViewModel viewModel);
        void ShowError(string v);
    }
}