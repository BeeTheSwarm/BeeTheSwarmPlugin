using System.Collections.Generic;
namespace BTS {
    public class GetEventsResponse : PackageResponse {
        public List<EventModel> Events { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Events = new List<EventModel>();
            var eventsSource = (List<object>)data["events"];
            foreach (object obj in eventsSource) {
                EventModel post = new EventModel();
                post.ParseJSON((Dictionary<string, object>)obj);
                Events.Add(post);
            }
        }
    }
}