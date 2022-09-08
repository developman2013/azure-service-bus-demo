using Azure.Messaging.ServiceBus;
using azure_service_bus_demo.Models;
using Newtonsoft.Json;

namespace azure_service_bus_demo.MessageBrokers;

public class ChatConsumer
{
    public delegate Task OnMessageRecivedHandler(MessageModel message);
    public event OnMessageRecivedHandler OnMessageRecived;

    private readonly ServiceBusProcessor _processor;

    public ChatConsumer(ServiceBusConsumerSettingsModel settings)
    {
        var client = new ServiceBusClient(settings.ConnectionString);

        _processor = client.CreateProcessor(topicName: settings.Topic, subscriptionName: settings.Subscription);

        _processor.ProcessMessageAsync += async (ProcessMessageEventArgs arg) =>
        {
            var message = JsonConvert.DeserializeObject<MessageModel>(arg.Message.Body.ToString())
                ?? throw new Exception("Incorrect JSON");

            OnMessageRecived?.Invoke(message);

            await arg.CompleteMessageAsync(arg.Message);
        };

        _processor.ProcessErrorAsync += OnMessageProcessingWithError;

        _processor.StartProcessingAsync().GetAwaiter().GetResult();
    }

    private Task OnMessageProcessingWithError(ProcessErrorEventArgs arg)
    {
        return Task.CompletedTask;
    }
}
