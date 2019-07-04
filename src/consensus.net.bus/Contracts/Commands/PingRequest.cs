using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace consensus.net.bus.Contracts
{
    public class PingRequest : SimpleRequest<PingRequest>
    {

        public PingRequest()
        {

        }

        private ILogger _logger;

        public PingRequest(ILogger logger)
        {
            _logger = logger;
        }
        
    }
}