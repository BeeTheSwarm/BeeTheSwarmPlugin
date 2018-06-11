using System.Collections.Generic;

namespace BTS {
    public class GameModel: DataModel {
        
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Image { get; private set; }
        public string Url { get; private set; }

        public override void ParseJSON(Dictionary<string, object> data) {
            Id = int.Parse(data["id"].ToString());
            Title = data["title"].ToString();
            Image = data["image"].ToString();
            Url = data["url"].ToString();
        }
    }
}