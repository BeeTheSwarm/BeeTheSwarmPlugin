using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToHiveController : TopPanelScreenController<IAddToHiveView>, IAddToHiveViewListener, IAddToHiveController 
{
    [Inject]
    private IUserProfileModel m_userModel;
    protected override bool BackButtonEnabled {
        get {
            return true;
        }
    }

    public override void Show() {
        base.Show();
        m_view.SetCode(m_userModel.User.HiveCode);
    }

    public AddToHiveController()
    { 
    }

    public void CopyCodeClicked()
    {
        
    }

    public void SendByMailClicked()
    {
        
    }

    public void SendByPhoneClicked()
    {
        
    }
}
