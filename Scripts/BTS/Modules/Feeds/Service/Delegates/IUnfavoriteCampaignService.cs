namespace BTS {
    public interface IUnfavoriteCampaignService: IService {
        void Execute(int postId);
    }
}