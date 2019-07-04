using MassTransit;
using System.Threading.Tasks;

namespace consensus.net.bus
{
    public interface IBusManager
    {
        void AddConsumer<T>(T consumer, string queueName) where T : class,IConsumer, new();

        IRequestClient<T> CreateRequestClient<T>(int timeoutSeconds = 60) where T : class;

        Task Publish<T>(T message) where T : class;


        void Dispose();
        void Start();

    }
}