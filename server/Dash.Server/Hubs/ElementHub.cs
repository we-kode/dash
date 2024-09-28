using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Dash.Server.Hubs;
public class ElementHub : Hub
{
    public async Task Subscribe(Guid elemId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{elemId}").ConfigureAwait(false);
    }
}
