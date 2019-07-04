using System;

namespace consensus.net.bus.Contracts {
    public abstract class Event : IMessage , IDisposable{
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }

        public string CorrelationId { get; set; }
        public Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public void Dispose()
        {
           
        }
    }

    public interface IMessage 
    {
        string CorrelationId { get; set; }
        Guid Id { get; set; }
        DateTime Timestamp { get; set; }
    }
}