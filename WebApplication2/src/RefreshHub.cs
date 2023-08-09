using Microsoft.AspNetCore.SignalR;

namespace TestTask.src
{
    public class RefreshHub : Hub
    {
        public async Task Refresh()
        {
            await Clients.All.SendAsync("refresh");
        }
    }
}
