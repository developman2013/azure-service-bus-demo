using azure_service_bus_demo.Hubs;
using azure_service_bus_demo.MessageBrokers;
using azure_service_bus_demo.Models;
using Microsoft.AspNetCore.SignalR;

namespace azure_service_bus_demo.Services;

public class ConsumerWorkerSingleton
{
    private readonly ChatConsumer _chatConsumer;
    private readonly IHubContext<MessageHub> _hubContext;

    public ConsumerWorkerSingleton(ChatConsumer chatConsumer, IHubContext<MessageHub> hubContext)
    {
        _chatConsumer = chatConsumer;
        _hubContext = hubContext;

        _chatConsumer.OnMessageRecived += async Task (MessageModel message) =>
        {
            await _hubContext.Clients.All.SendAsync("OnMessageRecived", message);
        };
    }
}
