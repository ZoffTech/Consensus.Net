using System;

namespace consensus.net.bus.Contracts
{
    public class HeartbeatResponseEvent
    {
        public string ServiceName { get; set; }

        public HeartbeatResponseEvent(Guid id) : this()
        {
            Id = id;
        }

        public HeartbeatResponseEvent()

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