using MassTransit;
using System;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts
{
    public abstract class SimpleResponse : IMessage 
    {
        public SimpleResponse(Guid id) : this()
        {
            Id = id;
        }

        public SimpleResponse()

        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();

            Timestamp = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string CorrelationId { get; set; }
    }
}