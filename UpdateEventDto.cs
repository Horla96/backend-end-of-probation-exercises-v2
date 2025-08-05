using event_scheduler_and_conflict_detector_api.Enums;

namespace event_scheduler_and_conflict_detector_api.Dtos
{
    public class UpdateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public List<string> Attendees { get; set; }
        public EventType EventType { get; set; }

    }
}
