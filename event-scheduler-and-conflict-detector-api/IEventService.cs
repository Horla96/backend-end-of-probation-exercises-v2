using event_scheduler_and_conflict_detector_api.Dtos;
using event_scheduler_and_conflict_detector_api.Models;

namespace event_scheduler_and_conflict_detector_api.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvent();
        Event GetEventById(Guid id);
        IEnumerable<Event> GetEventsByDate(DateTime date);
        IEnumerable<Event> GetEventsInRange(DateTime start, DateTime end);
        void AddEvent(CreateEventDto dto);
        void UpdateEvent(Guid id, UpdateEventDto dto);
        bool DeleteEvent(Guid id);

    }
}
