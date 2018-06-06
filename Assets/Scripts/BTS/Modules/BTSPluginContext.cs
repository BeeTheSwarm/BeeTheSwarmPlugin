using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BTS {
    public class BTSPluginContext : MonoBehaviour, IContext {
        public event Action OnInited = delegate { };
        private Dictionary<Type, IScreenController> m_controllersMap = new Dictionary<Type, IScreenController>();
        private Dictionary<Type, IPopupController> m_popupControllersMap = new Dictionary<Type, IPopupController>();
        private Dictionary<Type, IService> m_servicesMap = new Dictionary<Type, IService>();
        private Dictionary<Type, IModel> m_modelsMap = new Dictionary<Type, IModel>();
        private Dictionary<Type, Func<IViewController>> m_controllersDelegatesMap = new Dictionary<Type, Func<IViewController>>();

        private string m_notificationsId;
        internal void SetNotificationsId(string id)
        {
            m_notificationsId = id;
        }

        internal void StartPlugin(Action callback) {
            GetModel<IUserProfileModel>().OnUserLoggedIn += OnUserStatusChanged;
            GetService<IStartupService>().Execute(callback);
        }

        internal void AddBees(int count) {
            GetService<IAddBeesService>().Execute(count);
        }

        internal void Event(string levelId, int score) {
            GetService<ISendEventService>().Execute(levelId, score);
        }

        internal void GetEvents(Action<List<EventModel>> callback) {
            GetService<IGetEventsService>().Execute(callback);
        }

        private void OnUserStatusChanged() {
            Debug.Log("__One Signal  app ID:  " + m_notificationsId);

            #if !UNITY_EDITOR
            GetModel<IUserProfileModel>().OnUserLoggedIn -= OnUserStatusChanged;
            OneSignal.StartInit(m_notificationsId, null)
                .HandleNotificationOpened(OneSignalNotificationOpenedHandler)
                .HandleNotificationReceived(OneSignalNotificationReceivedHandler)
                .EndInit();
            OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
            OneSignal.permissionObserver += OnesignalPermisionObserver;
            var status = OneSignal.GetPermissionSubscriptionState();
            GetService<ISendPushIdService>().Execute(status.subscriptionStatus.userId);
#endif
        }

        private void OneSignalNotificationOpenedHandler(OSNotificationOpenedResult result) {
            
        }

        private void OneSignalNotificationReceivedHandler(OSNotification notification) {
            GetService<IUpdateNotificationsRealtime>().Execute();
        }
        

        private void OnesignalPermisionObserver(OSPermissionStateChanges stateChanges)
        {
            Debug.Log("__One Signal permission state changed");
            if (stateChanges.to.status == OSNotificationPermission.Authorized) {
                Debug.Log("__One signal autorized");
            }
        }

        internal void SetGameId(string gameId) {
            INetworkService network = GetService<INetworkService>();
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
            InjectDependencies();
            OnInited.Invoke();
        }

        private void InjectDependencies() {
            foreach (var controller in m_controllersMap.Values) {
                InjectFromMap(controller, m_modelsMap);
                InjectFromMap(controller, m_servicesMap);
                InjectFromMap(controller, m_controllersMap);
                InjectFromMap(controller, m_popupControllersMap);
                InjectControllerDelegates(controller);
                CheckInjects(controller);
                controller.PostInject();
            }
            foreach (var controller in m_popupControllersMap.Values) {
                InjectFromMap(controller, m_modelsMap);
                InjectFromMap(controller, m_servicesMap);
                InjectFromMap(controller, m_controllersMap);
                InjectFromMap(controller, m_popupControllersMap);
                CheckInjects(controller);
                controller.PostInject();
            }
            foreach (var service in m_servicesMap.Values) {
                InjectFromMap(service, m_modelsMap);
                InjectFromMap(service, m_servicesMap);
                CheckInjects(service);
                service.PostInject();
            }
        }

        private void CheckInjects(IInjectTarget target) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                Type fieldType = fieldInfo.FieldType;
                if (fieldInfo.GetValue(target)==null) {
                    Debug.Log("No injectable candidate for " + fieldType.Name + " in " + target.GetType().Name);
                }
            });
        }

        public void InjectToCommand(ICommand target) {
            InjectFromMap(target, m_modelsMap);
            InjectFromMap(target, m_servicesMap);
            target.PostInject();
        }

        private void InitServices() {
            AddService<INetworkService>(new NetworkService(this));
            AddService<IUserProfileService>(new UserProfileService());
            AddService<IFeedsService>(new FeedsService());
            AddService<IImagesService>(new ImagesService(this));
            AddService<IHistoryService>(new HistoryService());
            AddService<ICampaignCategoriesService>(new CampaignCategoriesService());
            AddService<INotificationsService>(new NotificationsService());
            AddService<IAddBeesService>(new AddBeesService());
            AddService<ISendPushIdService>(new SendPushIdService());
            AddService<IDeleteCampaignService>(new DeleteCampaignCommand());
            AddService<IGetUserCampaignService>(new GetUserCampaignCommand());
            AddService<IUpdateUserService>(new UpdateUserCommand());
            AddService<ICreateCampaignService>(new CreateCampaignCommand());
            AddService<ICreatePostService>(new CreatePostCommand());
            AddService<IGetHiveService>(new GetHiveCommand());
            AddService<IInviteFriendsPhoneService>(new InviteFriendsPhoneCommand());
            AddService<IInviteFriendsEmailService>(new InviteFriendsEmailCommand());
            AddService<IJoinHiveService>(new JoinHiveCommand());
            AddService<ISearchPlayerService>(new SearchPlayerCommand());
            AddService<IInviteToHiveService>(new InviteToHiveCommand());
            AddService<ILoginService>(new LoginCommand());
            AddService<IRegisterService>(new RegisterService());
            AddService<IResendCodeService>(new ResendCodeService());
            AddService<IConfirmRegistrationService>(new ConfirmRegistrationCommand());
            AddService<IInvitationResponseService>(new InvitationResponseCommand());
            AddService<IGetCampaingLevelsService>(new GetCampaingLevelsCommand());
            AddService<IGetImpactService>(new GetImpactCommand());
            AddService<IFeedCampaignService>(new FeedCampaignCommand());
            AddService<IGetPostCommentsService>(new GetPostComments());
            AddService<IGetCampaignsPostsService>(new GetCampaignsPostsCommand());
            AddService<ISendEventService>(new SendEventCommand());
            AddService<IGetEventsService>(new GetEventsCommand());
            AddService<IGetTopPostsService>(new GetTopPostsCommand());
            AddService<IGetFavoritePostsService>(new GetFavoritePostsCommand());
            AddService<IGetPostsService>(new GetPostsCommand());
            AddService<IGetUserService>(new GetUserCommand());
            AddService<IGetCampaignCategoriesService>(new GetCampaignCategoriesCommand());
            AddService<IGetCountersService>(new GetCounters());
            AddService<ILoadNotificationsCommand>(new LoadNotificationsCommand());
            AddService<ICreateCommentService>(new CreateCommentService());
            AddService<IFavoriteCampaignService>(new FavoriteCampaignCommand());
            AddService<IUpdatePostService>(new UpdatePostService());
            AddService<IUpdateCampaignService>(new UpdateCampaignService());
            AddService<IRequestsService>(new RequestsService());
            AddService<IGetRequestsCommand>(new GetRequestsCountCommand());
            AddService<ILoadRequestsCommand>(new LoadRequestsCommand());
            AddService<IDeletePostService>(new DeletePostCommand());
            AddService<IRemoveUserService>(new RemoveUserCommand());
            AddService<IGetOurGamesService>(new GetOurGamesService());
            AddService<IStartupService>(new StartupService());
            AddService<ILoadInitDataService>(new LoadInitDataService());
            AddService<IUnfavoriteCampaignService>(new UnfavoriteCampaignService());
            AddService<IUpdateNotificationsRealtime>(new UpdateNotificationsRealtimeService());
            AddService<IUpdateRequestsRealTimeService>(new UpdateRequestsRealtimeService());
            AddService<IProcessNotificationsRealtimeService>(new ProcessNotificationsRealtimeService());
            AddService<IResetPasswordService>(new ResetPasswordService());
            AddService<IConfirmResetPassword>(new ConfirmResetPasswordService());
            AddService<ISignOutService>(new SignOutService());
            AddService<IUpdateHiveService>(new UpdateHiveService());
            AddService<IGetSettingService>(new GetSettingsService());
            AddService<IGetTutorialRewardService>(new GetTutorialRewardService());
            AddService<IGetTutorialStateService>(new GetTutorialStateService());
        }
        
        private void InitModels() {
            m_modelsMap.Add(typeof(IUserProfileModel), new UserProfileModel());
            m_modelsMap.Add(typeof(IFeedsModel), new FeedsModel());
            m_modelsMap.Add(typeof(IEventsModel), new EventsModel());
            m_modelsMap.Add(typeof(ICampaignCategoriesModel), new CampaignCategoriesModel());
            m_modelsMap.Add(typeof(IImagesRepository), new ImagesRepository());
            m_modelsMap.Add(typeof(INotificationsModel), new NotificationsModel());
            m_modelsMap.Add(typeof(IPopupsModel), new PopupsModel());
            m_modelsMap.Add(typeof(IHiveModel), new HiveModel());
            m_modelsMap.Add(typeof(ILocalDataModel), new LocalDataModel());
            m_modelsMap.Add(typeof(IRequestsModel), new RequestsModel());
            m_modelsMap.Add(typeof(ISettingsModel), new SettingsModel());;
            m_modelsMap.Add(typeof(ITutorialModel), new TutorialModel());;
        }

        private void InitControllers() {
            m_controllersDelegatesMap.Add(typeof(ITopPanelController), () => new TopPanelControllerDelegate());
            m_controllersDelegatesMap.Add(typeof(IPostListControllerDelegate), () => new PostListControllerDelegate());

            AddController<IFeedsController>(new FeedsController(), GetView(typeof(FeedsView)));
            AddController<IUserProfileController>(new UserProfileScreenController(), GetView(typeof(UserProfileScreen)));
            AddController<IEditProfileController>(new EditProfileController(), GetView(typeof(EditProfileView)));
            AddController<IStartCampaignController>(new StartCampaignController(), GetView(typeof(StartCampaignView)));
            AddController<IUpdateCampaignController>(new UpdateCampaignController(), GetView(typeof(UpdateCampaignView)));
            AddController<IGreetingController>(new GreetingController(), GetView(typeof(GreetingView)));
            AddController<IPluginContentController>(new PluginContentController(), GetView(typeof(PluginContentView)));
            AddController<IAddToHiveController>(new AddToHiveController(), GetView(typeof(AddToHiveView)));
            AddController<IBadgesController>(new BadgesController(), GetView(typeof(BadgesView)));
            AddController<ICampaignToolboxController>(new CampaignToolboxController(), GetView(typeof(CampaignToolboxView)));
            AddController<IHiveController>(new HiveController(), GetView(typeof(HiveView)));
            AddController<ISameTypePostsController>(new SameTypePostsController(), GetView(typeof(SameTypePostsView)));
            AddController<IHiveLeaderboardController>(new HiveLeaderboardController(), GetView(typeof(HiveLeaderboardView)));
            AddController<IInviteFriendsController>(new InviteFriendsController(), GetView(typeof(InviteFriendsView)));
            AddController<INotificationsController>(new NotificationsController(), GetView(typeof(NotificationsView)));
            AddController<IOurGamesController>(new OurGamesController(), GetView(typeof(OurGamesView)));
            AddController<IQuestsController>(new QuestsController(), GetView(typeof(QuestsView)));
            AddController<ISearchMissingHivePlayersController>(new SearchMissingHivePlayersController(), GetView(typeof(SearchMissingHivePlayersView)));
            AddController<ISearchReffererController>(new SearchReffererController(), GetView(typeof(SearchReffererView)));
            AddController<IViewCampaignController>(new ViewCampaignController(), GetView(typeof(ViewCampaignView)));
            AddController<IBadgeAwardPopupController>(new BadgeAwardPopupController(), GetView(typeof(BadgeAwardPopupView)));
            AddController<IChestRevealController>(new ChestRevealController(), GetView(typeof(ChestRevealView)));
            AddController<IStoreController>(new StoreController(), GetView(typeof(StoreView)));
            AddController<ISignInController>(new SignInController(), GetView(typeof(SignInView)));
            AddController<ISignUpController>(new SignUpController(), GetView(typeof(SignUpView)));
            AddController<IUpdatePostController>(new UpdatePostController(), GetView(typeof(UpdatePostView)));
            AddController<IRequestsScreenController>(new RequestsScreenController(), GetView(typeof(RequestsScreenView)));
            AddController<IAddPostController>(new AddPostController(), GetView(typeof(AddPostView)));
            AddController<IForgotPasswordController>(new ForgotPasswordController(), GetView(typeof(ForgotPasswordView)));
            AddController<ITutorialController>(new TutorialController(), GetView(typeof(TutorialView)));

            AddPopupController<IRegistrationController>(new RegistrationController(), GetView(typeof(RegistrationView)));
            AddPopupController<ICampaignCategoriesController>(new CampaignCategoriesController(), GetView(typeof(CampaignCategoriesView)));
            AddPopupController<IDeleteCampaignPopupController>(new DeleteCampaignPopupController(), GetView(typeof(DeleteCampaignPopupView)));
            AddPopupController<IEditCampaignController>(new EditCampaignController(), GetView(typeof(EditCampaignView)));
            AddPopupController<IEditPostController>(new EditPostController(), GetView(typeof(EditPostView)));
            AddPopupController<IContactPickerController>(new ContactPickerController(), GetView(typeof(ContactPickerView)));
            AddPopupController<IAddFriendCodePopupController>(new AddFriendCodePopupController(), GetView(typeof(AddFriendCodePopupView)));
            AddPopupController<IInviteToHivePopupController>(new InviteToHivePopupController(), GetView(typeof(InviteToHivePopupView)));
            AddPopupController<IConfirmJoinHivePopupController>(new ConfirmJoinHivePopupController(), GetView(typeof(ConfirmJoinHivePopupView)));
            AddPopupController<ILoaderController>(new LoaderController(), GetView(typeof(LoaderView)));
            AddPopupController<IYesNoPopupController>(new YesNoPopupController(), GetView(typeof(YesNoPopupView)));
            AddPopupController<ISwarmAnimationController>(new SwarmAnimationController(), GetView(typeof(SwarmedBeeAnimation)));

        }

        public T GetService<T>() where T : IService {
            Type type = typeof(T);
            if (m_servicesMap.ContainsKey(type)) {
                return (T) m_servicesMap[type];
            }
            throw new ArgumentException("unknown service");
        }
        
        #if UNITY_EDITOR
        public void ReplaceService<T>(T service) where T : IService {
            m_servicesMap.Remove(typeof(T));
            AddService(service);
            InjectDependencies();
            
        }
        
        public void EmulateOneSignal() {
            OneSignalNotificationReceivedHandler(new OSNotification());
        }
        
        public void InjectAll(IInjectTarget target) {
            InjectFromMap(target, m_modelsMap);
            InjectFromMap(target, m_servicesMap);
            InjectFromMap(target, m_controllersMap);
            InjectFromMap(target, m_popupControllersMap);
            InjectControllerDelegates(target);
            CheckInjects(target);
            target.PostInject();
        }
        
        #endif
        
        private void AddService<T>(T service) where T : IService {
            m_servicesMap.Add(typeof(T), service);
        }

        private void AddController<T>(T controller, IView view) where T : IScreenController {
            controller.SetView(view);
            m_controllersMap.Add(typeof(T), controller);
        }

        private void AddPopupController<T>(T controller, IView view) where T : IPopupController {
            controller.SetView(view);
            m_popupControllersMap.Add(typeof(T), controller);
        }
        

        private void InjectFromMap<T>(IInjectTarget target, IDictionary<Type, T> sourceMap) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                var fieldType = fieldInfo.FieldType;
                if (sourceMap.ContainsKey(fieldType)) {
                    fieldInfo.SetValue(target, sourceMap[fieldType]);
                }
            });
        }
        
        private void InjectControllerDelegates(IInjectTarget target) {
            GetInjectableFields(target).ForEach(fieldInfo => {
                Type fieldType = fieldInfo.FieldType;
                if (m_controllersDelegatesMap.ContainsKey(fieldType)) {
                    fieldInfo.SetValue(target, GetControllerDelegate(m_controllersDelegatesMap[fieldType]));
                }
            });
            
        }
        

        private IViewController GetControllerDelegate(Func<IViewController> factory) {
            var controllerDelegate = factory();

            InjectFromMap(controllerDelegate, m_modelsMap);
            InjectFromMap(controllerDelegate, m_servicesMap);
            InjectFromMap(controllerDelegate, m_controllersMap);
            InjectFromMap(controllerDelegate, m_popupControllersMap);
            CheckInjects(controllerDelegate);
            controllerDelegate.PostInject();
            return controllerDelegate;
        }
        
        private List<FieldInfo> GetInjectableFields(object target) {
            var result = new List<FieldInfo>();
            Type type = target.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (var i = 0; i < fields.Length; i++) {
                var taggedFields = fields[i].GetCustomAttributes(typeof(Inject), false);
                if (taggedFields.Length > 0) {
                    result.Add(fields[i]);
                }
            }
            return result;
        }
                
        public T GetModel<T>() where T : IModel {
            Type type = typeof(T);
            if (m_modelsMap.ContainsKey(type)) {
                return (T)m_modelsMap[type];
            }
            throw new ArgumentException("unknown model");
        }

        
        private IView GetView(Type type) {
            return (IView)GetComponentInChildren(type, true);
        }

        void IContext.StartCoroutine(IEnumerator courutine) {
            StartCoroutine(courutine);
        }

        public void Show() {
            GetController<IPluginContentController>().Show();
        }

        public void Hide() {
            GetController<IPluginContentController>().Hide();
        }

        public T GetController<T>() where T : IScreenController {
            Type type = typeof(T);
            if (m_controllersMap.ContainsKey(type)) {
                return (T)m_controllersMap[type];
            }
            throw new ArgumentException("unknown controller");
        }

        public T CreateCommand<T>() where T : ICommand, new() {
            var command = Activator.CreateInstance<T>();
            InjectToCommand(command);
            return command;
        }
    }
}