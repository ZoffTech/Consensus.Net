using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts.Consumers
{
    public class HeartbeatResponseHandler : IConsumer<HeartbeatResponseEvent>
    {
        private ILogger _logger;

        public HeartbeatResponseHandler()
        {
            var factory = new LoggerFactory();
            _logger = factory.CreateLogger<HeartbeatResponseHandler>();
        }

        public HeartbeatResponseHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<HeartbeatResponseEvent> context)
        {
            _logger?.LogInformation($"{context.Message.ServiceName} Heartbeat Response Received");

            return Task.CompletedTask;
        }
    }
}