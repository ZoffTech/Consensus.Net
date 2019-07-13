using MassTransit;
using System;
using System.Collections.Generic;

namespace consensus.net.bus
{
    public class EventBusOptions : IEventBusOptions
    {
        public EventBusOptions()
        {
        }

        public String HostName { get; set; } = "rabbitmq://localhost";

        public String Username { get; set; } = "guest";

        public String Password { get; set; } = "guest";

        public string ServiceAddress { get; set; }
        public IEndpointDefinition QueueName { get; internal set; } 

        public List<IConsumer> Consumers { get; set; } = new List<IConsumer>();
    }
}