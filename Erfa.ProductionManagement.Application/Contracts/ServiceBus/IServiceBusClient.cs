namespace Erfa.ProductionManagement.Application.Contracts.ServiceBus
{
    public interface IServiceBusClient
    {
        Task PublishEventAsync<T>(string queue, T evt);
    }
}
