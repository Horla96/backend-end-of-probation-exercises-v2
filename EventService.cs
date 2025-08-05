using event_scheduler_and_conflict_detector_api.Dtos;
using event_scheduler_and_conflict_detector_api.Enums;
using event_scheduler_and_conflict_detector_api.Models;

namespace event_scheduler_and_conflict_detector_api.Services
{
    public class EventService : IEventService
    {
        private static readonly Dictionary<Guid, Event> _events = new();

        public EventService()
        {
            var event1 = new Event(new DateTime(2025, 06, 20, 12, 0, 0))
            {
                Id = Guid.NewGuid(),
                Title = "Workers meeting",
                Description = "Employess",
                Location = "office",
                Attendees = new List<string> { " OLA", "ATO" },
                EventType = EventType.Meeting
            };
            var event2 = new Event(new DateTime(2025, 06, 20, 15, 0, 0))
            {
                Id = Guid.NewGuid(),
                Title = "MD appointment",
                Description = "Employment",
                Location = "office",
                Attendees = new List<string> { "Ridwan" },
                EventType = EventType.Appointment
            };
            _events[event1.Id] = event1;
            _events[event2.Id] = event2;
        }

        public bool HasConflict(Event newEvent)
        {
            return _events.Values.Any(e =>
            e.Location.Equals(newEvent.Location, StringComparison.OrdinalIgnoreCase) &&
            e.StartTime < newEvent.EndTime && newEvent.StartTime < e.EndTime);
        }

        public void AddEvent(CreateEventDto dto)
        {
            var newEvent = new Event(dto.StartTime)
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Attendees = dto.Attendees,
                EventType = dto.EventType,
                StartTime = dto.StartTime
            };

            if (HasConflict(newEvent))
            {
                throw new InvalidOperationException("Event clashes with an existing event at same time and location");
            }
            _events[newEvent.Id] = newEvent;
        }

        public bool DeleteEvent(Guid id)
        {
            return _events.Remove(id);
        }

        public IEnumerable<Event> GetAllEvent()
        {
            var events = _events.Values;
            if (!events.Any())
            {
                throw new Exception("Events not found");
            }
            return events;
        }

        public Event GetEventById(Guid id)
        {
            if (!_events.TryGetValue(id, out var newEvent))
            {
                throw new KeyNotFoundException($"Event with ID: {id} not found");
            }
            return newEvent;
        }

        public IEnumerable<Event> GetEventsByDate(DateTime date)
        {
            return _events.Values
                .Where(e => e.StartTime.Date == date.Date)
                .ToList();

        }

        public IEnumerable<Event>GetEventsInRange(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Start date be earlier thn end date");

            return _events.Values
                .Where(e => e.StartTime >= start && e.StartTime <= end)
                .ToList();

        }

        public void UpdateEvent(Guid id, UpdateEventDto dto)
        {
            if (!_events.TryGetValue(id, out var events))
            {
                throw new KeyNotFoundException($"Event with ID: {id} not found");
            }
            var updatedEvent = new Event(dto.StartTime)
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Attendees = dto.Attendees,
                EventType = dto.EventType,
                StartTime = dto.StartTime
            };


            if (HasConflict(updatedEvent))
            {
                throw new InvalidOperationException("Event clashes with an existing event at same time and location");
            }
            _events[id] = updatedEvent;
        }
    }
}
