using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class FeedsService : BaseService, IFeedsService {
        [Inject] private IFeedsModel m_model;
        [Inject] private IUserProfileModel m_userModel;
        [Inject] private IGetCampaingLevelsService m_campaingLevelsService;
        [Inject] private IGetImpactService m_getImpactService;
        [Inject] private IPopupsModel m_popupsModel;

        [Inject] private IFeedCampaignService m_feedCampaignService;
        [Inject] private IGetPostCommentsService m_getPostCommentsService;
        [Inject] private IGetCampaignsPostsService m_getCampaignsPostsService;

        private void OnUserLoggedIn() {
            m_userModel.OnUserLoggedIn -= OnUserLoggedIn;
        //    GetCampaingLevels();
        //    GetImpact();
        }

        public void GetCampaingLevels() {
            m_campaingLevelsService.Execute();
        }

        public void GetImpact() {
            m_getImpactService.Execute();
        }

        public void FeedCampaign(int campaignId, int count) {
            m_feedCampaignService.Execute(campaignId, count);
        }

        public void UpdateCampaign(string title, string description, int category, Texture2D image, string address = "",
            string website = "", int charity = 0) {
        }

        public void GetPostComments(int postId, int offset, int limit, Action<List<CommentModel>> callback) {
            PostModel post = m_model.GetPost(postId);
            if (post.Comments.Count >= offset + limit) {
                callback(post.Comments.GetRange(offset, limit));
                return;
            }

            int offsetToLoad = Math.Min(post.Comments.Count, offset);
            int limitToLoad = offset + limit - offsetToLoad;
            if (limitToLoad == 0) {
                callback(new List<CommentModel>());
                return;
            }

            m_getPostCommentsService.Execute(postId, limitToLoad, offsetToLoad, (list, count) => {
                if (list == null) {
                    callback(new List<CommentModel>());
                    return;
                }

                m_model.AddPostComments(postId, list, count);
                callback.Invoke(post.Comments.GetRange(offset, Math.Min(limit, post.Comments.Count - offset)));
            });
        }

        public void GetCampaignsPosts(int campaign, int offset, int limit, Action<List<PostModel>> callback) {
            m_getCampaignsPostsService.Execute(campaign, offset, limit, callback);
        }

        public override void PostInject() {
            base.PostInject();
            m_userModel.OnUserLoggedIn += OnUserLoggedIn;
        }
    }
}