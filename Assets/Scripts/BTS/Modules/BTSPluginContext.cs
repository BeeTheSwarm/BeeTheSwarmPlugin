using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using BTS.Base.DependencyInjection;

namespace BTS {
    public class BTSPluginContext : MonoBehaviour, IContext {
        public event Action OnInited = delegate { };
        public event Action OnUserLoggedIn = delegate { };
        private DependencyContainer m_dependencyContainer = new DependencyContainer();

        private string m_notificationsId;
        internal void SetNotificationsId(string id)
        {
            m_notificationsId = id;
        }

        internal void StartPlugin(Action callback) {
            m_dependencyContainer.GetModel<IUserProfileModel>().OnUserLoggedIn += UserLoggedInHandler;
            m_dependencyContainer.GetService<IStartupService>().Execute(callback);
        }

        private void UserLoggedInHandler() {
            OnUserLoggedIn.Invoke();
        }

        internal void AddBees(int count) {
            m_dependencyContainer.GetService<IAddBeesService>().Execute(count);
        }

        internal void Event(string levelId, int score) {
            m_dependencyContainer.GetService<ISendEventService>().Execute(levelId, score);
        }

        internal void GetEvents(Action<List<EventModel>> callback) {
            m_dependencyContainer.GetService<IGetEventsService>().Execute(callback);
        }

        public void RegisterPushId(string id) {
            m_dependencyContainer.GetService<ISendPushIdService>().Execute(id);
        }
        
        public void ProcessNotificationPushId(string id) {
            m_dependencyContainer.GetService<ISendPushIdService>().Execute(id);
        }
        
        internal void SetGameId(string gameId) {
            INetworkService network = m_dependencyContainer.GetService<INetworkService>();
            network.SetGameId(gameId);
        }

        private void Awake() {
            DontDestroyOnLoad(gameObject);
            Init();
        }

        private void Init() {
            InitModels();
            InitServices();
            InitControllers();
            m_dependencyContainer.InjectDependencies();
            OnInited.Invoke();
        }

        
        private void InitServices() {
            m_dependencyContainer.AddService<INetworkService>(new NetworkService(this));
            m_dependencyContainer.AddService<IUserProfileService>(new UserProfileService());
            m_dependencyContainer.AddService<IFeedsService>(new FeedsService());
            m_dependencyContainer.AddService<IImagesService>(new ImagesService(this));
            m_dependencyContainer. AddService<IHistoryService>(new HistoryService());
            m_dependencyContainer.AddService<ICampaignCategoriesService>(new CampaignCategoriesService());
            m_dependencyContainer.AddService<INotificationsService>(new NotificationsService());
            m_dependencyContainer.AddService<IAddBeesService>(new AddBeesService());
            m_dependencyContainer.AddService<ISendPushIdService>(new SendPushIdService());
            m_dependencyContainer.AddService<IDeleteCampaignService>(new DeleteCampaignCommand());
            m_dependencyContainer.AddService<IGetUserCampaignService>(new GetUserCampaignCommand());
            m_dependencyContainer.AddService<IUpdateUserService>(new UpdateUserCommand());
            m_dependencyContainer.AddService<ICreateCampaignService>(new CreateCampaignCommand());
            m_dependencyContainer.AddService<ICreatePostService>(new CreatePostCommand());
            m_dependencyContainer.AddService<IGetHiveService>(new GetHiveCommand());
            m_dependencyContainer.AddService<IInviteFriendsPhoneService>(new InviteFriendsPhoneCommand());
            m_dependencyContainer.AddService<IInviteFriendsEmailService>(new InviteFriendsEmailCommand());
            m_dependencyContainer.AddService<IJoinHiveService>(new JoinHiveCommand());
            m_dependencyContainer.AddService<ISearchPlayerService>(new SearchPlayerCommand());
            m_dependencyContainer.AddService<IInviteToHiveService>(new InviteToHiveCommand());
            m_dependencyContainer.AddService<ILoginService>(new LoginCommand());
            m_dependencyContainer.AddService<IRegisterService>(new RegisterService());
            m_dependencyContainer.AddService<IResendCodeService>(new ResendCodeService());
            m_dependencyContainer.AddService<IConfirmRegistrationService>(new ConfirmRegistrationCommand());
            m_dependencyContainer.AddService<IInvitationResponseService>(new InvitationResponseCommand());
            m_dependencyContainer.AddService<IGetCampaingLevelsService>(new GetCampaingLevelsCommand());
            m_dependencyContainer.AddService<IGetImpactService>(new GetImpactCommand());
            m_dependencyContainer.AddService<IFeedCampaignService>(new FeedCampaignCommand());
            m_dependencyContainer.AddService<IGetPostCommentsService>(new GetPostComments());
            m_dependencyContainer.AddService<IGetCampaignsPostsService>(new GetCampaignsPostsCommand());
            m_dependencyContainer.AddService<ISendEventService>(new SendEventCommand());
            m_dependencyContainer.AddService<IGetEventsService>(new GetEventsCommand());
            m_dependencyContainer.AddService<IGetTopPostsService>(new GetTopPostsCommand());
            m_dependencyContainer.AddService<IGetFavoritePostsService>(new GetFavoritePostsCommand());
            m_dependencyContainer.AddService<IGetPostsService>(new GetPostsCommand());
            m_dependencyContainer.AddService<IGetUserService>(new GetUserCommand());
            m_dependencyContainer.AddService<IGetCampaignCategoriesService>(new GetCampaignCategoriesCommand());
            m_dependencyContainer.AddService<IGetCountersService>(new GetCounters());
            m_dependencyContainer.AddService<ILoadNotificationsCommand>(new LoadNotificationsCommand());
            m_dependencyContainer.AddService<ICreateCommentService>(new CreateCommentService());
            m_dependencyContainer.AddService<IFavoriteCampaignService>(new FavoriteCampaignCommand());
            m_dependencyContainer.AddService<IUpdatePostService>(new UpdatePostService());
            m_dependencyContainer.AddService<IUpdateCampaignService>(new UpdateCampaignService());
            m_dependencyContainer.AddService<IRequestsService>(new RequestsService());
            m_dependencyContainer.AddService<IGetRequestsCommand>(new GetRequestsCountCommand());
            m_dependencyContainer.AddService<ILoadRequestsCommand>(new LoadRequestsCommand());
            m_dependencyContainer.AddService<IDeletePostService>(new DeletePostCommand());
            m_dependencyContainer.AddService<IRemoveUserService>(new RemoveUserCommand());
            m_dependencyContainer.AddService<IGetOurGamesService>(new GetOurGamesService());
            m_dependencyContainer.AddService<IStartupService>(new StartupService());
            m_dependencyContainer.AddService<ILoadInitDataService>(new LoadInitDataService());
            m_dependencyContainer.AddService<IUnfavoriteCampaignService>(new UnfavoriteCampaignService());
            m_dependencyContainer.AddService<IUpdateNotificationsRealtime>(new UpdateNotificationsRealtimeService());
            m_dependencyContainer.AddService<IUpdateRequestsRealTimeService>(new UpdateRequestsRealtimeService());
            m_dependencyContainer.AddService<IProcessNotificationsRealtimeService>(new ProcessNotificationsRealtimeService());
            m_dependencyContainer.AddService<IResetPasswordService>(new ResetPasswordService());
            m_dependencyContainer.AddService<IConfirmResetPassword>(new ConfirmResetPasswordService());
            m_dependencyContainer.AddService<ISignOutService>(new SignOutService());
            m_dependencyContainer.AddService<IUpdateHiveService>(new UpdateHiveService());
            m_dependencyContainer.AddService<IGetSettingService>(new GetSettingsService());
            m_dependencyContainer.AddService<IGetTutorialRewardService>(new GetTutorialRewardService());
            m_dependencyContainer.AddService<IGetTutorialStateService>(new GetTutorialStateService());
        }
        
        private void InitModels() {
            m_dependencyContainer.AddModel<IUserProfileModel>(new UserProfileModel());
            m_dependencyContainer.AddModel<IFeedsModel>(new FeedsModel());
            m_dependencyContainer.AddModel<IEventsModel>(new EventsModel());
            m_dependencyContainer.AddModel<ICampaignCategoriesModel>(new CampaignCategoriesModel());
            m_dependencyContainer.AddModel<IImagesRepository>(new ImagesRepository());
            m_dependencyContainer.AddModel<INotificationsModel>(new NotificationsModel());
            m_dependencyContainer.AddModel<IPopupsModel>(new PopupsModel());
            m_dependencyContainer.AddModel<IHiveModel>(new HiveModel());
            m_dependencyContainer.AddModel<ILocalDataModel>(new LocalDataModel());
            m_dependencyContainer.AddModel<IRequestsModel>(new RequestsModel());
            m_dependencyContainer.AddModel<ISettingsModel>(new SettingsModel());;
            m_dependencyContainer.AddModel<ITutorialModel>(new TutorialModel());;
        }

        private void InitControllers() {
            m_dependencyContainer.AddControllerDelegate<ITopPanelController>(() => new TopPanelControllerDelegate());
            m_dependencyContainer.AddControllerDelegate<IPostListControllerDelegate>(() => new PostListControllerDelegate());

            m_dependencyContainer.AddController<IFeedsController>(new FeedsController(), GetView(typeof(FeedsView)));
            m_dependencyContainer.AddController<IUserProfileController>(new UserProfileScreenController(), GetView(typeof(UserProfileScreen)));
            m_dependencyContainer.AddController<IEditProfileController>(new EditProfileController(), GetView(typeof(EditProfileView)));
            m_dependencyContainer.AddController<IStartCampaignController>(new StartCampaignController(), GetView(typeof(StartCampaignView)));
            m_dependencyContainer.AddController<IUpdateCampaignController>(new UpdateCampaignController(), GetView(typeof(UpdateCampaignView)));
            m_dependencyContainer.AddController<IGreetingController>(new GreetingController(), GetView(typeof(GreetingView)));
            m_dependencyContainer.AddController<IPluginContentController>(new PluginContentController(), GetView(typeof(PluginContentView)));
            m_dependencyContainer.AddController<IAddToHiveController>(new AddToHiveController(), GetView(typeof(AddToHiveView)));
            m_dependencyContainer.AddController<IBadgesController>(new BadgesController(), GetView(typeof(BadgesView)));
            m_dependencyContainer.AddController<ICampaignToolboxController>(new CampaignToolboxController(), GetView(typeof(CampaignToolboxView)));
            m_dependencyContainer. AddController<IHiveController>(new HiveController(), GetView(typeof(HiveView)));
            m_dependencyContainer.AddController<ISameTypePostsController>(new SameTypePostsController(), GetView(typeof(SameTypePostsView)));
            m_dependencyContainer.AddController<IHiveLeaderboardController>(new HiveLeaderboardController(), GetView(typeof(HiveLeaderboardView)));
            m_dependencyContainer.AddController<IInviteFriendsController>(new InviteFriendsController(), GetView(typeof(InviteFriendsView)));
            m_dependencyContainer.AddController<INotificationsController>(new NotificationsController(), GetView(typeof(NotificationsView)));
            m_dependencyContainer.AddController<IOurGamesController>(new OurGamesController(), GetView(typeof(OurGamesView)));
            m_dependencyContainer.AddController<IQuestsController>(new QuestsController(), GetView(typeof(QuestsView)));
            m_dependencyContainer.AddController<ISearchMissingHivePlayersController>(new SearchMissingHivePlayersController(), GetView(typeof(SearchMissingHivePlayersView)));
            m_dependencyContainer.AddController<ISearchReffererController>(new SearchReffererController(), GetView(typeof(SearchReffererView)));
            m_dependencyContainer.AddController<IViewCampaignController>(new ViewCampaignController(), GetView(typeof(ViewCampaignView)));
            m_dependencyContainer.AddController<IBadgeAwardPopupController>(new BadgeAwardPopupController(), GetView(typeof(BadgeAwardPopupView)));
            m_dependencyContainer.AddController<IChestRevealController>(new ChestRevealController(), GetView(typeof(ChestRevealView)));
            m_dependencyContainer.AddController<IStoreController>(new StoreController(), GetView(typeof(StoreView)));
            m_dependencyContainer.AddController<ISignInController>(new SignInController(), GetView(typeof(SignInView)));
            m_dependencyContainer.AddController<ISignUpController>(new SignUpController(), GetView(typeof(SignUpView)));
            m_dependencyContainer.AddController<IUpdatePostController>(new UpdatePostController(), GetView(typeof(UpdatePostView)));
            m_dependencyContainer.AddController<IRequestsScreenController>(new RequestsScreenController(), GetView(typeof(RequestsScreenView)));
            m_dependencyContainer.AddController<IAddPostController>(new AddPostController(), GetView(typeof(AddPostView)));
            m_dependencyContainer.AddController<IForgotPasswordController>(new ForgotPasswordController(), GetView(typeof(ForgotPasswordView)));
            m_dependencyContainer.AddController<ITutorialController>(new TutorialController(), GetView(typeof(TutorialView)));

            m_dependencyContainer.AddPopupController<IRegistrationController>(new RegistrationController(), GetView(typeof(RegistrationView)));
            m_dependencyContainer.AddPopupController<ICampaignCategoriesController>(new CampaignCategoriesController(), GetView(typeof(CampaignCategoriesView)));
            m_dependencyContainer.AddPopupController<IDeleteCampaignPopupController>(new DeleteCampaignPopupController(), GetView(typeof(DeleteCampaignPopupView)));
            m_dependencyContainer.AddPopupController<IEditCampaignController>(new EditCampaignController(), GetView(typeof(EditCampaignView)));
            m_dependencyContainer. AddPopupController<IEditPostController>(new EditPostController(), GetView(typeof(EditPostView)));
            m_dependencyContainer.AddPopupController<IContactPickerController>(new ContactPickerController(), GetView(typeof(ContactPickerView)));
            m_dependencyContainer.AddPopupController<IAddFriendCodePopupController>(new AddFriendCodePopupController(), GetView(typeof(AddFriendCodePopupView)));
            m_dependencyContainer.AddPopupController<IInviteToHivePopupController>(new InviteToHivePopupController(), GetView(typeof(InviteToHivePopupView)));
            m_dependencyContainer.AddPopupController<IConfirmJoinHivePopupController>(new ConfirmJoinHivePopupController(), GetView(typeof(ConfirmJoinHivePopupView)));
            m_dependencyContainer.AddPopupController<ILoaderController>(new LoaderController(), GetView(typeof(LoaderView)));
            m_dependencyContainer.AddPopupController<IYesNoPopupController>(new YesNoPopupController(), GetView(typeof(YesNoPopupView)));
            m_dependencyContainer.AddPopupController<ISwarmAnimationController>(new SwarmAnimationController(), GetView(typeof(SwarmedBeeAnimation)));

        }
       
        
        private IView GetView(Type type) {
            return (IView)GetComponentInChildren(type, true);
        }

        void IContext.StartCoroutine(IEnumerator courutine) {
            StartCoroutine(courutine);
        }

        public T CreateCommand<T>() where T : ICommand, new() {
            return m_dependencyContainer.CreateCommand<T>();
        }

        public void Show() {
            m_dependencyContainer.GetController<IPluginContentController>().Show();
        }

        public void Hide() {
            m_dependencyContainer.GetController<IPluginContentController>().Hide();
        }

        
    }
}