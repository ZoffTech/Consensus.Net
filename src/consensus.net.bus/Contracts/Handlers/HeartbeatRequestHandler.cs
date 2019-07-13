using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts.Consumers
{
    public class HeartbeatRequestHandler : IConsumer<HeartbeatRequestEvent>
    {
        private ILogger _logger;
        private IConfiguration _config;

        public HeartbeatRequestHandler()
        {
          
            
        }
        public HeartbeatRequestHandler(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public Task Consume(ConsumeContext<HeartbeatRequestEvent> context)
        {
            _logger?.LogInformation("Heartbeat Request Received");

            context.Publish<HeartbeatResponseEvent>(new HeartbeatResponseEvent
            {
                ServiceName = AppData.HostName,
                Id = context.Message.Id,
            }); 

            return Task.CompletedTask;
        }
    }
}
