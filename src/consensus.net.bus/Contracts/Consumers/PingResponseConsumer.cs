using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts.Consumers
{
    public class PingResponseConsumer : IConsumer<PingResponse>
    {
        private ILogger _logger;
        public PingResponseConsumer()
        {
            _logger = AppData.Logger;
        }
        public PingResponseConsumer(ILogger<PingResponse> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<PingResponse> context)
        {

            _logger?.LogInformation($"{context.Message.ServiceName} Ping Response Received");
          
            return Task.CompletedTask;
        }
    }
}
