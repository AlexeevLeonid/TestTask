using Microsoft.AspNetCore.SignalR;

namespace Test66bit2.src
{
    public class RefreshHub : Hub
    {
        public async Task Refresh()
        {
            await Clients.All.SendAsync("refresh");
        }
    }
}
