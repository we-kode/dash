using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Dash.Server.Hubs;
public class PinboardHub : Hub
{
    public async Task Subscribe()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "dash.pinboard").ConfigureAwait(false);
    }
}
