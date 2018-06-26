namespace BTS {
    public class GetTutorialRewardService: BaseNetworkService<GetUserResponce>, IGetTutorialRewardService {
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private ITutorialModel m_tutorialModel;
        
        protected override void HandleSuccessResponse(GetUserResponce data) {
            m_userModel.SetBees(data.User.Bees);
            m_userModel.SetLevel(data.User.Level, data.User.Progress);
            m_tutorialModel.IsTutorialAvailable = false;
        }

        public void Execute(int score) {
            SendPackage(new BTS_FinishTutorial(score));    
        }
    }
}