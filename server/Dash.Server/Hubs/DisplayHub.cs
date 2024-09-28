using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Dash.Server.Hubs;
public class DisplayHub : Hub
{
    public async Task Subscribe(Guid displayId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{displayId}").ConfigureAwait(false);
    }
}
