using consensus.net.bus.Contracts.Consumers;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace consensus.net.bus
{
    public class BusManagerOptions
    {
        public BusManagerOptions(ILogger<BusManagerOptions> logger,PingResponseConsumer pingResponseConsumer = null,
            PingRequestConsumer pingRequestConsumer=null)
        {
            HostName = "rabbitmq://localhost";

            if (pingRequestConsumer != null)
                Consumers.Add(pingRequestConsumer);

            if (pingResponseConsumer != null)
                Consumers.Add(pingResponseConsumer);

        }
        public String HostName { get; set; }
        public string ServiceAddress { get; set; }

        public List<IConsumer> Consumers = new List<IConsumer>();
    }
}
