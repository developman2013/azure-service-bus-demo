using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace AzureServiceBusSender;

public class Program
{
    private static readonly string _connectionString = "Endpoint=sb://...";
    private static readonly string _topic = "direct-messages-topic";

    public static async Task Main()
    {
        var sender = new ServiceBusClient(_connectionString).CreateSender(_topic);

        Console.WriteLine("Your username:");
        var username = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("Type your message and press Enter:");
        while (true)
        {
            var message = Console.ReadLine();
            var messageJson = JsonConvert.SerializeObject(new MessageModel
            {
                 Message = message,
                 Username = username
            });
            await sender.SendMessageAsync(new ServiceBusMessage(messageJson));
        }
    }
}
