using azure_service_bus_demo.Hubs;
using azure_service_bus_demo.MessageBrokers;
using azure_service_bus_demo.Models;
using azure_service_bus_demo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials()
      .WithOrigins("https://localhost:44498");
}
));

// Setup Azure Service Bus consumer
var chatConsumerConfiguration = builder.Configuration.GetSection("ChatConsumer").Get<ServiceBusConsumerSettingsModel>();
builder.Services.AddSingleton(new ChatConsumer(chatConsumerConfiguration));
builder.Services.AddSingleton<ConsumerWorkerSingleton>();



var app = builder.Build();

    app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapHub<MessageHub>("/chatHub");

app.MapFallbackToFile("index.html");

// Start singleton worker
app.Services.GetService<ConsumerWorkerSingleton>();

app.Run();
