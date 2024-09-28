using Dash.Application.Contracts;
using Dash.Server.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using Dash.Persistence.Models;
using Dash.Server.Contracts;
using System.Collections.Generic;

namespace Dash.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController(IHubContext<CalendarHub> hubContext, ICalendarRepository repository) : ControllerBase
    {
        private const string CALENDAR_REFRESH = "CALENDAR_REFRESH";
        private const string CALENDAR_GROUP_NAME = "dash.cal";

        // get
        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult<Calendar> Get(Guid id)
        {
            var cal = repository.Get(id);
            if (cal == null)
            {
                return NotFound();
            }

            return cal;
        }

        [HttpGet]
        //[Authorize]
        public List<Calendar> Get()
        {
            return repository.Get();
        }

        // create calendar
        [HttpPost]
        //[Authorize]
        public ActionResult Create([FromBody] CalendarRequest req)
        {
            repository.Create(req.Name, req.Color);
            return Ok();
        }

        // update calendar
        [HttpPost("{id:Guid}")]
        //[Authorize]
        public ActionResult Update(Guid id, [FromBody] CalendarRequest req)
        {
            if (!repository.Exists(id))
            {
                return NotFound();
            }
            repository.Update(id, req.Name, req.Color);
            return Ok();
        }


        // delete calendar
        [HttpDelete("{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            repository.DeleteCalendar(id);
            await hubContext.Clients.Group(CALENDAR_GROUP_NAME).SendAsync(CALENDAR_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // get
        [HttpGet("event/{id:Guid}")]
        //[Authorize]
        public ActionResult<CalendarEvent> GetEvent(Guid id)
        {
            var cal = repository.GetEvent(id);
            if (cal == null)
            {
                return NotFound();
            }

            return cal;
        }

        // ccreate cEvent
        [HttpPost("event")]
        //[Authorize]
        public async Task<ActionResult> CreateEvent([FromBody] CalendarEventRequest req)
        {
            repository.CreateEvent(req.Summary, req.Description, req.Location, req.DtStart, req.DtEnd, req.CalendarId);
            await hubContext.Clients.Group(CALENDAR_GROUP_NAME).SendAsync(CALENDAR_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // update cEvent
        [HttpPost("event/{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult> UpdateEvent(Guid id, [FromBody] CalendarEventRequest req)
        {
            if (!repository.ExistsEvent(id))
            {
                return NotFound();
            }
            repository.UpdateEvent(id, req.Summary, req.Description, req.Location, req.DtStart, req.DtEnd, req.CalendarId);
            await hubContext.Clients.Group(CALENDAR_GROUP_NAME).SendAsync(CALENDAR_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // delete cEvent
        [HttpDelete("event/{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            repository.DeleteCalendarEvent(id);
            await hubContext.Clients.Group(CALENDAR_GROUP_NAME).SendAsync(CALENDAR_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // load cEvents by date range
        [HttpGet("events")]
        public List<CalendarEvent> GetEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return repository.ListEvents(start, end);
        }
    }
}
