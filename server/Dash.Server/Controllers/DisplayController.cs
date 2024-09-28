using Dash.Application.Contracts;
using Dash.Persistence.Models;
using Dash.Server.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Dash.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisplayController(IHubContext<DisplayHub> hubContext, IDisplayRepository repository) : ControllerBase
    {
        // Use in future typed hub.
        private const string DASH_DISPLAY_REFRESH = "DASH_DISPLAY_REFRESH";

        /// <summary>
        /// Refreshes the display
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("refresh/{id:Guid}")]
        //[Authorize]
        public async Task<ActionResult> Refresh(Guid id)
        {
            await hubContext.Clients.Group($"{id}").SendAsync(DASH_DISPLAY_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Updates the settings of the display
        /// </summary>
        /// <param name="display"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> Update([FromBody] Display display)
        {
            if (!repository.Exists(display.DisplayId))
            {
                return NotFound();
            }

            repository.CreateOrUpdate(display);
            await hubContext.Clients.Group($"{display.DisplayId}").SendAsync(DASH_DISPLAY_REFRESH).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Loads the display to be shown with share id.
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        [HttpGet("Edit")]
        // [Authorize]
        public ActionResult<Display> Edit()
        {
            var display = repository.GetDisplay(IDisplayRepository.DefaultDisplayId);
            if (display == null)
            {
                return NotFound();
            }
            return display;
        }

        /// <summary>
        /// Loads the display to be shown with share id.
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        [HttpGet("{shareId:Guid}")]
        public ActionResult<Display> Get(Guid shareId)
        {
            var display = repository.GetDisplayByShareId(shareId);
            if (display == null)
            {
                return NotFound();
            }
            return display;
        }
    }
}
