using BTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class ViewCampaignController : TopPanelScreenController<IViewCampaignView>, IViewCampaignViewListener, IViewCampaignController {
        [Inject] private IFeedsService m_feedService;
        [Inject] private IFeedsModel m_feedsModel;
        [Inject] private IInviteFriendsController m_inviteFriendsController;
        [Inject] private IPostListControllerDelegate m_postsControllerDelegate;
        [Inject] private IPopupsModel m_popupsModel;

        public ViewCampaignController() {
        }


        protected override bool BackButtonEnabled
        {
            get { return true; }
        }

        public override void PostInject() {
            base.PostInject();
            m_postsControllerDelegate.SetView(m_view.GetPostlistContainer());
            m_postsControllerDelegate.SetMaxItems(100);
            m_postsControllerDelegate.PostsClickable = false;
        }

        public void Show(int campaignId) {
            base.Show();
            m_postsControllerDelegate.Clear();
            m_postsControllerDelegate.PostsEditable = true;
            m_postsControllerDelegate.SetItemsSource((offset, limit, callback) => { m_feedService.GetCampaignsPosts(campaignId, offset, limit, callback); });
            m_postsControllerDelegate.Update();
        }

        public void OnInviteClick() {
            m_inviteFriendsController.Show();
        }
    }
}