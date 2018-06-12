using UnityEngine;
using System.Collections;
using System;
namespace BTS {

    public class TopPanelControllerDelegate : ITopPanelController {
        [Inject]
        private IUserProfileModel m_userProfileModel;
        [Inject]
        private INotificationsModel m_notificationsModel;
        [Inject]
        private IUserProfileService m_userProfileService;
        [Inject]
        private IImagesService m_imagesService;
        private TopPanelViewModel m_viewModel;
        public event Action OnBackBtnPressed = delegate { };
        public event Action OnAvatarPressed = delegate { };

        public TopPanelControllerDelegate() {
            m_viewModel = new TopPanelViewModel();
            m_viewModel.AvatarClickCallback = () => { OnAvatarPressed.Invoke(); };
            m_viewModel.BackButtonCallback = () => { OnBackBtnPressed.Invoke(); };
        }
        
        public void PostInject() {
            m_userProfileModel.OnLevelUpdated += m_viewModel.Level.Set;
            m_userProfileModel.OnBeesCountUpdated += m_viewModel.Bees.Set;
            if (m_userProfileModel.IsLoggedIn) {
                OnGotUserInfo();
            }
            else {
                m_userProfileModel.OnUserUpdated += OnGotUserInfo;
            }
            m_viewModel.Notifications.Set(m_notificationsModel.NewNotifications);
            m_notificationsModel.NotificationsCountUpdated += m_viewModel.Notifications.Set;
        }


        public void SetView(IView view) {
            ((ITopPanelComponent)view).SetViewModel(m_viewModel);

        }

        public void SetAvatarEnabled(bool value) {
            m_viewModel.AvatarEnabled.Set(value);
        }

        public void SetBackButtonEnabled(bool value) {
            m_viewModel.BackButtonEnabled.Set(value);
        }

        private void OnGotUserInfo() {
            UserModel data = m_userProfileModel.User;
            if (data == null) {
                return;
            }

            data.OnAvatarChanged += () => {
                m_imagesService.GetImage(data.Avatar, m_viewModel.Avatar.Set);
            };
            if (data.Avatar != null && m_viewModel.AvatarEnabled.Get()) {
                m_imagesService.GetImage(data.Avatar, m_viewModel.Avatar.Set);
            }
            m_viewModel.Bees.Set(data.Bees);
            m_viewModel.Level.Set(data.Level);
        }
        
    }

}