using MassTransit;
using System.Collections.Generic;

namespace consensus.net.bus
{
    public interface IEventBusOptions
    {
        string HostName { get; set; }
        string Password { get; set; }
        IEndpointDefinition QueueName { get; }
        string ServiceAddress { get; set; }
        string Username { get; set; }

        List<IConsumer> Consumers { get; set; }
    }
}