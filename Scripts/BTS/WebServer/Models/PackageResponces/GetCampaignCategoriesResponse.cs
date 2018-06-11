using System.Collections.Generic;
namespace BTS {
    public class GetCampaignCategoriesResponse : PackageResponse {
        public List<CategoryModel> Categories { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Categories = new List<CategoryModel>();
            List<object> commentsSource = (List<object>)data["categories"];
            foreach (var commentSource in commentsSource) {
                CategoryModel category = new CategoryModel();
                category.ParseJSON((Dictionary<string, object>)commentSource);
                Categories.Add(category);
            }
        }
    }
}