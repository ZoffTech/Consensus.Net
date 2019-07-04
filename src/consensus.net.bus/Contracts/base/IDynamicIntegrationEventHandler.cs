using System.Threading.Tasks;

namespace consensus.net.bus.Contracts
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}