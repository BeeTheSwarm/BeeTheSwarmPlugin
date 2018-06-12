using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFriendCodePopupController : BasePopupController<IAddFriendCodePopupView>, IAddFriendCodePopupViewListener, IAddFriendCodePopupController {
    public AddFriendCodePopupController() {

    }
    private AddFriendCodePopupViewModel m_viewModel = new AddFriendCodePopupViewModel();
    public void Show(Action<object> callback, object options) {

    }

    protected override void PostSetView() {
        base.PostSetView();
        m_view.SetViewModel(m_viewModel);
    }
    private Action<string> m_callback;
    public void Show(Action<string> callback) {
        m_viewModel.Code = string.Empty;
        m_callback = callback;
        base.Show();
    }

    public void OnBackClicked() {
        m_callback.Invoke(string.Empty);
        Hide();
    }

    public void OnAddClicked() {
        m_callback.Invoke(m_viewModel.Code);
        Hide();
    }
}
