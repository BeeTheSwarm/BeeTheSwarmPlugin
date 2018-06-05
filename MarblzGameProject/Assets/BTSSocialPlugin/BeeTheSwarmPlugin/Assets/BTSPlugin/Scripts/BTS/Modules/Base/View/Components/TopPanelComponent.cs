using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class TopPanelComponent : BaseView, ITopPanelComponent
{
    [SerializeField]
    private BTS.Avatar m_avatar;
    [SerializeField]
    private GameObject m_avatarContainer;
    [SerializeField]
    private Text m_beesCount;
    [SerializeField]
    private Text m_level;
    [SerializeField]
    private Button m_backButton;
    [SerializeField]
    private Text m_notificationsCount;
    [SerializeField]
    private GameObject m_notificationsBlock;
        
    private void SetBees(int bees)
    {
        m_beesCount.text = bees.ToString();
    }

    private void SetLevel(int level)
    {
        m_level.text = "Level " + level.ToString();
    }

    public void OnAvatarClicked()
    {
        m_viewModel.AvatarClickCallback.Invoke();
    }

    public void OnBackClicked()
    {
        m_viewModel.BackButtonCallback.Invoke();
    }

    public void SetListener(IViewEventListener baseController) {
        throw new NotImplementedException();
    }

    public void Show() {
        throw new NotImplementedException();
    }

    public void Hide() {
        throw new NotImplementedException();
    }
    private TopPanelViewModel m_viewModel;
    public void SetViewModel(TopPanelViewModel viewModel) {
        m_viewModel = viewModel;
        m_viewModel.Bees.Subscribe(SetBees);
        m_viewModel.Level.Subscribe(SetLevel);
        m_viewModel.Avatar.Subscribe(m_avatar.SetAvatar);
        m_viewModel.Notifications.Subscribe(SetNotifications);
        m_viewModel.AvatarEnabled.Subscribe(m_avatarContainer.SetActive);
        m_viewModel.BackButtonEnabled.Subscribe(m_backButton.gameObject.SetActive);
    }

    private void SetNotifications(int count) {
        if (count <= 0) {
            m_notificationsBlock.gameObject.SetActive(false);
        } else {
            m_notificationsBlock.gameObject.SetActive(true);
            m_notificationsCount.text = count.ToString();
        }
    }
}
