using consensus.net.bus;
using consensus.net.bus.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consensus.net.service_registry.Listeners
{
    public class PingConsumer : IConsumer<PingRequest>
    {
        public Task Consume(ConsumeContext<PingRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
