using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using BTS;

public class HistoryService : BaseService, IHistoryService
{
    private List<IScreenController> m_history = new List<IScreenController>();
    [Inject]
    private IUserProfileModel m_userModel;

    public override void PostInject() {
        base.PostInject();
        m_userModel.OnUserLoggedOut += LogoutHandler;
    }

    private void LogoutHandler() {
        HideCurrent();
        m_history.Clear();
    }

    public void AddItem(IScreenController controller)
    {
        HideCurrent();
        m_history.Add(controller);
    }

    private void HideCurrent()
    {
        if (m_history.Count > 0)
        {
            m_history[m_history.Count - 1].Hide();
        }
    }

    public void BackPressedItem(IScreenController controller)
    {
        controller.Hide();
        m_history.Remove(controller);
        if (m_history.Count > 0) {
            var restored = m_history[m_history.Count - 1];
            m_history.RemoveAt(m_history.Count - 1);
            restored.Restore();
        }
    }
}
