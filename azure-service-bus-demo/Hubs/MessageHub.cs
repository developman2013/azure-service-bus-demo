using Microsoft.AspNetCore.SignalR;

namespace azure_service_bus_demo.Hubs;

public class MessageHub : Hub 
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
