using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFriendCodePopupView : BaseControlledView<IAddFriendCodePopupViewListener>, IAddFriendCodePopupView {


    private AddFriendCodePopupViewModel m_viewModel;
    public void CodeEndEditHandler(string text) {
        m_viewModel.Code = text;
    }

    public void BackBtnClickHandler() {
        m_controller.OnBackClicked();
    }

    public void AddBtnClickHandler() {
        m_controller.OnAddClicked();
    }

    public void SetViewModel(AddFriendCodePopupViewModel viewModel) {
        m_viewModel = viewModel ;
    }
}
