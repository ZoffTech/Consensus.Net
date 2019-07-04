using System.Threading.Tasks;

namespace consensus.net.bus.Contracts
{
    public interface IIntegrationEventHandler<T> where T : Event
    {
        Task Handle(T eventData);
    }
}