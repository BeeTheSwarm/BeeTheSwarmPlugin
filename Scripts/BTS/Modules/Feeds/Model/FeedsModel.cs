using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS {
    public class FeedsModel : IFeedsModel {
        public event Action OnCampaignsUpdated = delegate { };
        public event Action OnCampaignCreated = delegate { };
        public event Action<float> OnImpactUpdated = delegate { };
        public event Action<int> OnCampaignDeleted = delegate { };
        public event Action<int> OnPostDeleted = delegate { };
        private PostsList m_campaignsList = new PostsList();
        private PostsList m_favoriteCampaignsList = new PostsList();
        private PostsList m_topCampaignsList = new PostsList();
        private PostsList m_userCampaign = new PostsList();
        private Dictionary<int, PostModel> m_postsCache = new Dictionary<int, PostModel>();

        public PostsList CampaignsList
        {
            get { return m_campaignsList; }
        }
        public PostsList UserCampaign
        {
            get { return m_userCampaign; }
        }
        public PostsList FavoriteCampaignsList
        {
            get { return m_favoriteCampaignsList; }
        }
        public PostsList TopCampaignsList
        {
            get { return m_topCampaignsList; }
        }
        private float m_impact;

        public float Impact
        {
            get { return m_impact; }
            set
            {
                m_impact = value;
                OnImpactUpdated.Invoke(value);
            }
        }

        public bool LevelsLoaded
        {
            get
            {
                return m_campaignsLevels.Count > 0;
            }
        }
        
        private List<CampaignLevelModel> m_campaignsLevels= new List<CampaignLevelModel>();

        public FeedsModel() {
        }

        public void SetCampainLevels(List<CampaignLevelModel> levels) {
            m_campaignsLevels = levels;
        }

        public void AddPostComments(int postId, List<CommentModel> comments, int totalComments) {
            List<PostModel> posts = FindPosts(postId);
            posts.ForEach(post => {
                post.UpdateCommentsCount(totalComments);
                post.AddComments(comments);
            });
        }

        private List<PostModel> FindPosts(int postId) {
            List<PostModel> result = new List<PostModel>();
            result.AddRange(TopCampaignsList.FindPosts(postId));
            result.AddRange(CampaignsList.FindPosts(postId));
            result.AddRange(FavoriteCampaignsList.FindPosts(postId));
            if (UserCampaign != null) {
                result.AddRange(UserCampaign.FindPosts(postId));
            }

            return result;
        }

        public CampaignLevelModel GetCampaignLevel(int levelId) {
            if (m_campaignsLevels != null) {
                return m_campaignsLevels.Find(level => { return level.Level == levelId; });
            }

            return null;
        }

        public int GetRaisedMoneyForLevel(int levelId) {
            int sum = 0;
            if (m_campaignsLevels != null) {
                m_campaignsLevels.FindAll(level => level.Level <= levelId).ForEach(level => sum += level.Amount);
            }
            return sum;
        }
        
        public void UpdatePostUserFed(PostModel post) {
            var aPost = GetPost(post.Id);
            if (aPost != null) {
                aPost.Campaign.Update(post.Campaign);
            }
        }

        public void UpdatePostAddComment(int postId, CommentModel commentModel) {
            var post = GetPost(postId);
            if (post != null) {
                post.UpdateCommentsCount(post.CommentsCount + 1);
                post.AddNewComment(commentModel);
            }
        }

        public PostModel GetPost(int postId) {
            if (m_postsCache.ContainsKey(postId)) {
                return m_postsCache[postId];
            }

            return null;
        }

        public int? GetCampaignByPostId(int postId) {
            var post = GetPost(postId);
            if (post == null) {
                return null;
            }

            return post.Campaign.Id;
        }

        public void AddUserCampaign(List<PostModel> list) {
            UpdatePosts(list);
            m_userCampaign.AddPosts(list);
        }


        public void UpdatePosts(List<PostModel> dataPosts) {
            var t = dataPosts.GetRange(0, dataPosts.Count);
            dataPosts.Clear();
            t.ForEach(post => {
                if (m_postsCache.ContainsKey(post.Id)) {
                    m_postsCache[post.Id].Update(post);
                    dataPosts.Add(m_postsCache[post.Id]);
                }
                else {
                    m_postsCache.Add(post.Id, post);
                    dataPosts.Add(post);
                }
            });
        }

        public void AddNewCampaign(List<PostModel> dataPosts) {
            AddUserCampaign(dataPosts);
            m_campaignsList.InsertPosts(dataPosts);
            OnCampaignCreated.Invoke();
        }

        public void SetCampaignFavorite(CampaignModel campaign) {
            campaign.SetIsFavorite(true);
            m_favoriteCampaignsList.InsertPosts(GetPostByCampaign(campaign).GetRange(0, 1));
        }

        public void UpdateCampaings(CampaignModel campaign) {
            var posts = m_postsCache.Values.Where(post => post.Campaign.Id == campaign.Id).ToList();
            posts.ForEach(post => { post.Campaign.Update(campaign); });
            OnCampaignsUpdated.Invoke();
        }

        public void AddUserPost(PostModel dataPost) {
            m_postsCache.Add(dataPost.Id, dataPost);
            m_userCampaign.InsertPosts(new List<PostModel>() {dataPost});
            var post = m_favoriteCampaignsList.GetPosts().Find(apost => apost.Campaign.Id == dataPost.Campaign.Id);
            if (post != null) {
                m_favoriteCampaignsList.RemovePostsByCampaign(post.Campaign.Id);
                m_favoriteCampaignsList.InsertPosts(new List<PostModel>() {dataPost});
            }

            post = m_topCampaignsList.GetPosts().Find(apost => apost.Campaign.Id == dataPost.Campaign.Id);
            if (post != null) {
                m_topCampaignsList.RemovePostsByCampaign(post.Campaign.Id);
                m_topCampaignsList.InsertPosts(new List<PostModel>() {dataPost});
            }

            post = m_campaignsList.GetPosts().Find(apost => apost.Campaign.Id == dataPost.Campaign.Id);
            if (post != null) {
                m_campaignsList.RemovePostsByCampaign(post.Campaign.Id);
            }

            m_campaignsList.InsertPosts(new List<PostModel>() {dataPost});
        }

        public void DeletePost(int postId) {
            if (m_postsCache.ContainsKey(postId)) {
                m_postsCache.Remove(postId);
                m_favoriteCampaignsList.Clear();
                m_topCampaignsList.Clear();
                m_campaignsList.Clear();
                m_userCampaign.RemovePost(postId);
                OnPostDeleted.Invoke(postId);
            }
        }

        public void RemoveCampaignFromFavorite(CampaignModel campaign) {
            campaign.SetIsFavorite(false);
            m_favoriteCampaignsList.RemovePostsByCampaign(campaign.Id);
        }

        public void SignOut() {
            m_favoriteCampaignsList.Clear();
            m_topCampaignsList.Clear();
            m_userCampaign.Clear();
        }

        private List<PostModel> GetPostByCampaign(CampaignModel campaign) {
            var result = new List<PostModel>();
            foreach (var postModel in m_postsCache.Values) {
                if (postModel.Campaign.Id == campaign.Id) {
                    result.Add(postModel);
                }
            }

            return result;
        }

        public void DeleteUserCampaign() {
            var posts = UserCampaign.GetPosts();
            if (posts.Count > 0) {
                var campaignId = posts[0].Campaign.Id;
                posts.ForEach(post => {
                    m_campaignsList.RemovePost(post.Id);
                    m_favoriteCampaignsList.RemovePost(post.Id);
                    m_topCampaignsList.RemovePost(post.Id);
                    m_userCampaign.RemovePost(post.Id);
                    m_postsCache.Remove(post.Id);
                });
                OnCampaignDeleted.Invoke(campaignId);
            }
        }
    }
}