using System.Collections.Generic;
namespace BTS {
    public class GetEventsResponse : PackageResponse {
        public List<EventModel> Events { get; private set; }
        public List<ProgressModel> Progress { get; private set; }
        public override void Parse(Dictionary<string, object> data) {
            Events = new List<EventModel>();
            var eventsSource = (List<object>)data["events"];
            foreach (object obj in eventsSource) {
                EventModel eventModel = new EventModel();
                eventModel.ParseJSON((Dictionary<string, object>)obj);
                Events.Add(eventModel);
            }
            Progress = new List<ProgressModel>();
            var progressSource = (List<object>)data["progress"];
            foreach (object obj in progressSource) {
                ProgressModel progress = new ProgressModel();
                progress.ParseJSON((Dictionary<string, object>)obj);
                Progress.Add(progress);
            }
        }
    }
}