using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts.Consumers
{
    public class PingRequestConsumer : IConsumer<PingRequest>
    {
        private ILogger _logger;
        private IConfiguration _config;

        public PingRequestConsumer()
        {
            _logger = AppData.Logger;
            
        }
        public PingRequestConsumer(ILogger<PingRequest> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public Task Consume(ConsumeContext<PingRequest> context)
        {
            _logger?.LogInformation("Ping Request Received");

            context.Publish<PingResponse>(new PingResponse
            {
                ServiceName = AppData.HostName,
                Id = context.Message.Id,
               

            }); 

            return Task.CompletedTask;
        }
    }
}
