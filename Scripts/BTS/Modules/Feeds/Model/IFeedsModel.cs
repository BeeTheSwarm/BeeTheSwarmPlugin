using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface IFeedsModel : IModel
    {
        PostsList CampaignsList { get; }
        PostsList FavoriteCampaignsList { get; }
        PostsList TopCampaignsList { get; }
        PostsList UserCampaign { get; }

        int? GetCampaignByPostId(int postId);

        void AddPostComments(int postId, List<CommentModel> comments, int totalComments);
        void SetCampainLevels(List<CampaignLevelModel> levels);

        CampaignLevelModel GetCampaignLevel(int level);
        int GetRaisedMoneyForLevel(int levelId);
        float Impact { get; set; }
        bool LevelsLoaded { get; }
        

        event Action<int> OnCampaignDeleted;
        event Action OnCampaignCreated;
        event Action OnCampaignsUpdated;
        event Action<int> OnPostDeleted;
        event Action<float> OnImpactUpdated;

        void UpdatePostUserFed(PostModel post);
        void UpdatePostAddComment(int postId, CommentModel commentModel);

        PostModel GetPost(int postId);

        void DeleteUserCampaign();
        void AddUserCampaign(List<PostModel> list);
        void UpdatePosts(List<PostModel> dataPosts);
        void AddNewCampaign(List<PostModel> dataPosts);
        void SetCampaignFavorite(CampaignModel campaign);
        void UpdateCampaings(CampaignModel dataPosts);
        void AddUserPost(PostModel dataPost);
        void DeletePost(int postId);
        void RemoveCampaignFromFavorite(CampaignModel campaign);
        void SignOut();
    }
}