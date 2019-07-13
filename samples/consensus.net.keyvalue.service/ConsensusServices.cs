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
        private IEventBus _events;
        private ILogger<ConsensusServices> _logger;

        public ConsensusServices(IEventBus events,ILogger<ConsensusServices> logger)

        {
            logger.LogInformation("Starting Consumer Services");
            _events = events;
            _logger = logger;
           
        }
    }
}
