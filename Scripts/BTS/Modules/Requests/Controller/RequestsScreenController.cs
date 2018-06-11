using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestsScreenController : BaseScreenController<IRequestsScreenView>, IRequestsScreenViewListener, IRequestsScreenController {
    [Inject]
    private IRequestsService m_requestsService;    [Inject]
    private IRequestsModel m_requestsModel;
    [Inject]
    private IImagesService m_imageService;
    [Inject] private ILoaderController m_loader;

    [Inject]
    private IInvitationResponseService m_invitationResponseService;
    private RequestsScreenViewModel m_viewModel = new RequestsScreenViewModel();
    private bool m_waitingResponce;
    private const int REQUEST_DECLINED = 0;
    private const int REQUEST_ACCEPTED = 1;

    public RequestsScreenController() {

    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_viewModel);
    }

    public override void PostInject() {
        base.PostInject();
        m_requestsModel.OnRequestRemoved += m_viewModel.RemoveRequest;
    }

    public override void Show() {
        base.Show();
        LoadNextNotifications();
    }

    private void RequestsReceived(List<InvitationModel> list) {
        m_loader.Hide();
        list.ForEach(item => m_viewModel.Requests.Add(CreateViewModel(item)));
        m_waitingResponce = false;
    }

    private RequestItemViewModel CreateViewModel(InvitationModel item) {
        RequestItemViewModel viewModel = new RequestItemViewModel(item.Id, item.User.Name, item.Type);
        viewModel.OnAccept += OnRequestAccepted;
        viewModel.OnDecline += OnRequestDeclined;
        m_imageService.GetImage(item.User.Avatar, viewModel.UserAvatar.Set);
        return viewModel;
    }


    private void OnRequestDeclined(RequestItemViewModel viewModel) {
        m_invitationResponseService.Execute(viewModel.Id, REQUEST_DECLINED);
        viewModel.OnDecline -= OnRequestDeclined;
        viewModel.OnAccept -= OnRequestAccepted;
    }

    private void OnRequestAccepted(RequestItemViewModel viewModel) {
        m_invitationResponseService.Execute(viewModel.Id, REQUEST_ACCEPTED);
        viewModel.OnDecline -= OnRequestDeclined;
        viewModel.OnAccept -= OnRequestAccepted;
    }

    private void LoadNextNotifications() {
        if (!m_waitingResponce) {
            m_loader.Show("Loading...");
            m_waitingResponce = true;
            m_requestsService.GetRequests(m_viewModel.Requests.Count(), 20, RequestsReceived);
        }
    }

    public void OnScrolledToEnd() {
        LoadNextNotifications();
    }
}
