using Erfa.ProductionManagement.Application.Contracts.ServiceBus;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Erfa.ProductionManagement.ServiceBus.RabbitMQ
{
    public class RabbitMQClient : IServiceBusClient
    {
        public RabbitMQSettings _settings { get; }

        public RabbitMQClient(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;

            if (_settings is null)
            {
                throw new NullReferenceException("settings.Value.cannot be null");
            }
        }

        public async Task PublishEventAsync<T>(string routingKey, T evt)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.HostPort,
                VirtualHost = _settings.VirtualHost,
                UserName = _settings.UserName,
                Password = _settings.Password,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateChannel();
            channel.QueueDeclare(routingKey, durable: true, exclusive: false, autoDelete: false);

            var jsonString = JsonSerializer.Serialize(evt);
            var body = Encoding.UTF8.GetBytes(jsonString);
            await channel.BasicPublishAsync("", routingKey, body: body);
            await channel.CloseAsync();
        }
    }
}
