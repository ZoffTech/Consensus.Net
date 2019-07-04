using consensus.net.bus;
using consensus.net.bus.Contracts;
using consensus.net.bus.Contracts.Consumers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consensus.net.keyvalue
{
    public class ConsensusServices
    {
        private IBusManager _bus;
        private ILogger<ConsensusServices> _logger;

        public ConsensusServices(IBusManager manager,ILogger<ConsensusServices> logger)

        {
            logger.LogInformation("Starting Consumer Services");
            _bus = manager;
            _logger = logger;
           // _bus.AddConsumer(new PingRequestConsumer(logger), "ping_queue");
        }
    }
}
