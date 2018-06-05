using UnityEngine;

namespace BTS {
    internal class UpdateCampaignService : BaseNetworkService<GetCampaignResponse>, IUpdateCampaignService {

        [Inject] private IFeedsModel m_feedModel;
        
        protected override void HandleSuccessResponse(GetCampaignResponse data) {
            m_feedModel.UpdateCampaings(data.Campaign);
        }

        public void Execute(string title, int category, string website) {
            SendPackage(new BTS_UpdateCampaign(title, category, website));
        }
    }
}