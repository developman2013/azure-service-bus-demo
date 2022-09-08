namespace azure_service_bus_demo.Models
{
    public class ServiceBusConsumerSettingsModel
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
        public string Subscription { get; set; }
    }
}
