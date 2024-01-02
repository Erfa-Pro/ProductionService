using Erfa.ProductionManagement.Application.Contracts.ServiceBus;
using Erfa.ProductionManagement.ServiceBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erfa.ProductionManagement.ServiceBus
{
    public static class ServiceBusServiceRegistration
    {
        public static IServiceCollection AddServiceBusServics(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSingleton<IServiceBusClient, RabbitMQClient>();
            services.Configure<RabbitMQSettings>(configuration.GetSection(RabbitMQSettings.Position));

            return services;
        }
    }
}