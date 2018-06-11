using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BTS;
using UnityEngine;
using UnityEngine.UI;

namespace BTS {

    internal class HiveView : TopPanelScreen<IHiveViewListener>, IHiveView {
        [SerializeField]
        private Text m_membersCount;
        [SerializeField]
        private Text m_membersRank;
        [SerializeField]
        private Text m_impact;
        [SerializeField]
        private Text m_impactRank;
        [SerializeField]
        private HiveMember m_hiveItem;
        [SerializeField]
        private Transform m_hiveContainer;
        private List<GameObject> m_hiveGameObjects = new List<GameObject>();
        [SerializeField]
        private Image m_referrerAvatar;
        [SerializeField]
        private GameObject m_referrerAvatarContainer;
        [SerializeField]
        private GameObject m_addCodeButtonReferrer;
        [SerializeField]
        private GameObject m_noUsersMessage;
        [SerializeField]
        private GameObject m_viewAllButton;

        private HiveViewModel m_viewModel;

        public void AddToHiveBtnClickHandler() {
            m_controller.AddToHiveClicked();
        }

        public void ViewAllBtnClkickHandler() {
            m_controller.ViewAllClicked();
        }

        public void RefferedByFriendBtnClickHandler() {
            m_controller.RefferedByFriendClicked();
        }

        public void SetViewModel(HiveViewModel viewModel) {
            m_viewModel = viewModel;
            viewModel.HasRefferer.Subscribe(HasReffererChangeHandler);
            viewModel.ReffererAvatar.Subscribe(SetReffereAvatar);
            viewModel.RankByMembers.Subscribe(SetRankByMembers);
            viewModel.RankByImpact.Subscribe(SetRankByImpact);
            viewModel.Members.Subscribe(SetMembersCount);
            viewModel.MembersList.OnAdd += MemberAddedHandler;
            viewModel.MembersList.OnClear += HiveClearedHandler;
            viewModel.Impact.Subscribe(SetImpact);
            SetButtonsVisibility();
        }

        private void HiveClearedHandler() {
            m_hiveGameObjects.ForEach(item => {
                Destroy(item.gameObject);
            });
            m_hiveGameObjects.Clear();
        }

        private void SetImpact(float value) {
            m_impact.text = value.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        }

        private void SetButtonsVisibility() {
            m_noUsersMessage.SetActive(m_viewModel.MembersList.Count() == 0);
            m_viewAllButton.SetActive(m_viewModel.MembersList.Count() > 9);
            m_hiveContainer.gameObject.SetActive(m_viewModel.MembersList.Count() != 0);
        }

        private void MemberAddedHandler(HiveMemberViewModel value) {
            AddItem(value);
            SetButtonsVisibility();
        }

        private void AddItem(HiveMemberViewModel value) {
            HiveMember item = GameObjectInstatiator.InstantiateFromObject(m_hiveItem);
            item.transform.SetParent(m_hiveContainer, false);
            item.SetViewModel(value);
            item.transform.SetAsLastSibling();
            m_hiveGameObjects.Add(item.gameObject);
            GameObject mock = new GameObject();
            mock.AddComponent<RectTransform>();
            mock.transform.SetParent(m_hiveContainer, false);
            mock.transform.SetAsLastSibling();
            m_hiveGameObjects.Add(mock.gameObject);
        }

        private void SetMembersCount(int value) {
            m_membersCount.text = value.ToString();
        }

        private void SetRankByImpact(int value) {
            m_impactRank.text = "Rank #"+value.ToString();
        }

        private void SetRankByMembers(int value) {
            m_membersRank.text = "Rank #" + value.ToString();
        }

        private void SetReffereAvatar(Sprite value) {
            m_referrerAvatar.overrideSprite = value;
        }

        private void HasReffererChangeHandler(bool value) {
            m_addCodeButtonReferrer.SetActive(!value);
            m_referrerAvatarContainer.SetActive(value);
        }
    }
}