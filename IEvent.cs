using event_scheduler_and_conflict_detector_api.Enums;

namespace event_scheduler_and_conflict_detector_api.Interface
{
    public interface IEvent
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public List<string> Attendees { get; set; }
        public EventType EventType { get; set; }
    }
}
