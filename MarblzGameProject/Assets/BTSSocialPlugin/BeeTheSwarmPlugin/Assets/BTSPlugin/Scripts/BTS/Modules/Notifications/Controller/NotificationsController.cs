using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsController : BaseScreenController<INotificationsView>, INotificationsViewListener, INotificationsController {
    [Inject]
    private INotificationsService m_notificationsService;
    [Inject] private INotificationsModel m_notificationsModel;
    [Inject] private IImagesService m_imageService;
    [Inject] private ILoaderController m_loader;
    [Inject] private IUserProfileModel m_userModel;

    private NotificationsScreenViewModel m_viewModel = new NotificationsScreenViewModel();
    private bool m_waitingResponce;
    public NotificationsController() {
        m_viewModel.NotificationsLoaded.Set(false);
    }

    public override void PostInject() {
        base.PostInject();
        m_userModel.OnUserStateChanged += StateChangedHandler;
    }

    private void StateChangedHandler(UserState state) {
        if (state == UserState.Unknown) {
            m_viewModel.Notifications.Clear();
            m_viewModel.NotificationsLoaded.Set(false);
        }
    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_viewModel);
    }
    
    public override void Show() {
        base.Show();
        if (!m_viewModel.NotificationsLoaded.Get()) {
            m_loader.Show("Loading...");
            LoadNextNotifications();
        }
        m_notificationsModel.SetNewNotificationsCount(0);
    }

    private void NotificationsReceived(List<NotificationModel> list) {
        m_loader.Hide();
        list.ForEach(item => m_viewModel.Notifications.Add(CreateViewModel(item)));
        m_viewModel.NotificationsLoaded.Set(true);
        m_waitingResponce = false;
    }

    private NotificationViewModel CreateViewModel(NotificationModel item) {
        NotificationViewModel viewModel;
        switch (item.Action) {
            case NotificationAction.CampaignFunded:
            case NotificationAction.DonateToCampaign:
                var postViewModel = new NotificationViewModel(item);
                viewModel = postViewModel;
                break;
            case NotificationAction.CommentToPost:
            case NotificationAction.ReplyToComment:
                var commentViewModel = new CommentNotificationViewModel(item);
                m_imageService.GetImage(((PostRelatednotificationModel)item).Post.Image, commentViewModel.PostImage.Set);
                viewModel = commentViewModel;
                break;
            default:
                viewModel = new NotificationViewModel(item);
                break;

        }
        m_imageService.GetImage(item.User.Avatar, viewModel.UserAvatar.Set);
        return viewModel;
    }

    private void LoadNextNotifications() {
        if (!m_waitingResponce) {
            m_waitingResponce = true;
            m_notificationsService.GetNotifications(m_viewModel.Notifications.Count(), 20, NotificationsReceived);
        }
    }

    public void OnScrolledToEnd() {
        LoadNextNotifications();
    }
}
