using Dash.Application.Contracts;
using Dash.Persistence.Models;
using Dash.Server.Contracts;
using Dash.Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dash.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementController(IHubContext<ElementHub> hubContext, IElementRepository repository) : ControllerBase
    {
        private const string DASH_DISPLAY_REFRESH = "DASH_DISPLAY_REFRESH";

        // create element
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult> Create([FromBody] ElementRequest req)
        {
            repository.Create(req.Config, req.Content, req.ComponentId, req.DisplayId, req.ExpireDate);
            await hubContext.Clients.Group($"{req.DisplayId}").SendAsync(DASH_DISPLAY_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // udpate element
        [HttpPost("{id:Guid}")]
        // [Authorize]
        public async Task<ActionResult> Update(Guid id, [FromBody] ElementRequest req)
        {
            repository.Update(id, req.Config, req.Content, req.ComponentId, req.DisplayId, req.ExpireDate);
            await hubContext.Clients.Group($"{req.DisplayId}").SendAsync(DASH_DISPLAY_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // delete element
        [HttpDelete("{id:Guid}/{displayId:Guid}")]
        // [Authorize]
        public async Task<ActionResult> Delete(Guid id, Guid displayId)
        {
            repository.Delete(id);
            await hubContext.Clients.Group($"{displayId}").SendAsync(DASH_DISPLAY_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        // get element
        [HttpGet("{displayId:Guid}")]
        public List<Element> GetByDisplay(Guid displayId)
        {
            return repository.GetByDisplay(displayId);
        }

        // get components
        [HttpGet("components")]
        //[Authorize]
        public List<Component> Components()
        {
            return repository.GetComponents();
        }
    }
}
