using MassTransit;
using System;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts
{
    public abstract class SimpleRequest<T> : IMessage 
    {
        public SimpleRequest(Guid id) : this()
        {
            Id = id;
        }

        public SimpleRequest()

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