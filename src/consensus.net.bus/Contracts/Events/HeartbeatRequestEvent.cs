using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace consensus.net.bus.Contracts
{
    public class HeartbeatRequestEvent : Event
    {

        public HeartbeatRequestEvent()
        {

        }

        private ILogger _logger;

        public HeartbeatRequestEvent(ILogger logger)
        {
            _logger = logger;
        }
        
    }
}