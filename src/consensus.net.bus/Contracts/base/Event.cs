using MassTransit;
using System;
using System.Threading.Tasks;

namespace consensus.net.bus.Contracts {
    public abstract class Event :  IDisposable
    {
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


    public class GenericEvent 
    {
        public GenericEvent()
        {

        }
    }



    public abstract class EventHandler<T> : IConsumer<T> where T : class, new()
    {
        public abstract Task Consume(ConsumeContext<T> context);
    }


    public abstract class GenericEventHandler : EventHandler<GenericEvent>
    {
        public GenericEventHandler()
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