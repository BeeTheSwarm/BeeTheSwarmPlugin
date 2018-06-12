using System;

namespace BTS {
    public class GetTutorialStateService : BaseNetworkService<GetTutorialStateResponce>, IGetTutorialStateService {
        [Inject] private ITutorialModel m_tutorialModel;

        protected override void HandleSuccessResponse(GetTutorialStateResponce data) {
            m_tutorialModel.IsTutorialAvailable = data.TutorialAvailable;
            FireSuccessFinishEvent();
        }

        public void Execute() {
            SendPackage(new BTS_GetTutorialState());
        }
    }
}