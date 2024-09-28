using Dash.Application.Contracts;
using Dash.Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Dash.Persistence.Models;

namespace Dash.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinboardController(IHubContext<PinboardHub> hubContext, IInformationRepository repository) : ControllerBase
    {
        // Use in future typed hub.
        private const string PINBOARD_REFRESH = "PINBOARD_REFRESH";
        private const string PINBOARD_GROUP_NAME = "dash.pinboard";

        [HttpGet("newId")]
        //[Authorize]
        public Guid NewId()
        {
            return Guid.NewGuid();
        }

        [HttpGet]
        public List<Information> List()
        {
            return repository.Get();
        }

        // load info list
        [HttpGet("list")]
        //[Authorize]
        public List<Information> ListInfoItems()
        {
            return repository.GetList();
        }

        // delete info
        [HttpDelete]
        //[Authorize]
        public async Task<ActionResult> Delete([FromBody] List<Guid> ids)
        {
            repository.Delete(ids);
            await hubContext.Clients.Group(PINBOARD_GROUP_NAME).SendAsync(PINBOARD_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // create/update info
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> Save([FromBody] Information data)
        {
            repository.CreateOrUpdate(data);
            await hubContext.Clients.Group(PINBOARD_GROUP_NAME).SendAsync(PINBOARD_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // get one info
        [HttpGet("{id:Guid}")]
        //[Authorize]
        public ActionResult<Information> Get(Guid id)
        {
            var data = repository.Get(id);
            if (data == null)
            {
                return NotFound();
            }

            return data;
        }
    }
}
