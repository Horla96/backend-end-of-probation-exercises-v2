using event_scheduler_and_conflict_detector_api.Dtos;
using event_scheduler_and_conflict_detector_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace event_scheduler_and_conflict_detector_api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("get all Events")]
        public ActionResult GetAllEvent()
        {
            try
            {
                var events = _eventService.GetAllEvent();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("id")]
        public ActionResult GetEventById(Guid id)
        {
            try
            {
                var events = _eventService.GetEventById(id);
                return Ok(events);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("range")]
        public ActionResult GetEventInRanges(DateTime start, DateTime end)
        {
            var events = _eventService.GetEventsInRange(start, end);

            if (!events.Any())
            {
                return NotFound("No events found in the range");
            }
            return Ok(events);
        }

        [HttpPost("add an event")]
        public IActionResult AddEvent(CreateEventDto dto)
        {
            try
            {
                _eventService.AddEvent(dto);
                return Ok("Event Added");


            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

      [HttpPut("{id:guid}")]
        public IActionResult UpdateEvent(Guid id, UpdateEventDto dto)
        {
            try
            {
                _eventService.UpdateEvent(id, dto);
                return Ok("Event Updated Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteEvent(Guid id)
        {
            var deleted = _eventService.DeleteEvent(id);
            if (!deleted)
            {
                return NotFound($"Event with ID: {id} not found");
            }

            return Ok("Event Deleted");
        }
    }
}
