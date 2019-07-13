using consensus.net.bus.Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace consensus.net.bus
{
    /// <summary>
    /// This class is the fundation to handle subscription to Messages sent by services across the ecosystem.
    ///
    /// </summary>

    public class EventBus : IDisposable, IEventBus
    {
        public bool Started;

        protected ILogger<EventBus> _logger;
        private IServiceProvider _container;

        protected IConsensusOptions _options;
        private string _queueName;

        public EventBus(
            IServiceProvider container,
            ILogger<EventBus> logger)
        {
            _options = container.GetService<IConsensusOptions>();// options;
            _logger = logger;
            _container = container;

            //     Start();
        }

        protected IRabbitMqBusFactoryConfigurator _busConfigurator { get; private set; }

        protected IBusControl _busControl { get; private set; }

        protected IRabbitMqHost _host { get; private set; }

        protected bool disposed { get; private set; }

        public void AddSubscriber<T>(T Consumer, string queueName) where T : class, IConsumer, new()
        {
            using (_logger.BeginScope("Adding Consumer..."))
            {
                _busConfigurator.ReceiveEndpoint(_host, queueName,

                       e =>
                       {
                           e.Consumer<T>();
                       });

                _logger.LogInformation($"Consumer {typeof(T).Name} added using {queueName} queue");
            }
        }

        public void Publish<T>(T message) where T : class
        {
            var submit = _busControl.Publish<T>(message);
            submit.Wait();
        }

        public IRequestClient<T> CreateRequestClient<T>(int timeoutSeconds = 60) where T : class
        {
            return _busControl.CreateRequestClient<T>(TimeSpan.FromSeconds(10));
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        public virtual void Start()
        {
            using (_logger.BeginScope("Starting Bus..."))
            {
                _busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    var host = sbc.Host(new Uri(_options.EventBusOptions.HostName), $"consensus.net.events.{AppData.HostName}", c =>
                    {
                        c.Password(_options.EventBusOptions.Password);
                        c.Username(_options.EventBusOptions.Username);
                    });
                    sbc.ExchangeType = ExchangeType.Fanout;
                    sbc.Durable = false;
                    sbc.ReceiveEndpoint(host, "consensus.net.event.bus", e =>
                 {
                     foreach (var c in _options.EventBusOptions.Consumers)
                     {
                         var t = c.GetType();

                         e.Consumer(t, type => _container.GetService(type));

                         _logger.LogInformation($"Added event consumer: {t.Name}");
                     }
                 });

                    _busConfigurator = sbc;
                    _host = host;
                });

                _busControl.Start(); // This is important!

                _logger.LogInformation("Bus Started");

                Started = true;
            }
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            using (_logger.BeginScope("Stopping Bus..."))
            {
                // Check to see if Dispose has already been called.
                if (!this.disposed)
                {
                    // If disposing equals true, dispose all managed
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.
                        _busControl.Stop();
                    }

                    // Note disposing has been done.
                    disposed = true;
                }
            }
        }

        public void Publish(Event @event)
        {
            _busControl.Publish(@event);
        }

        public void Subscribe<T, TH>()
            where T : Event, new()
            where TH : Contracts.EventHandler<T>, new()
        {
            using (_logger.BeginScope("Adding Subscription..."))
            {
                _busConfigurator.ReceiveEndpoint(_host, _options.EventBusOptions.QueueName,

                       e =>
                       {
                           e.Consumer<TH>();
                       });

                _logger.LogInformation($"Subscription to {typeof(T).Name} added using {_queueName} queue");
            }
        }

        public void SubscribeGeneric<TH>(string eventName) where TH : GenericEventHandler, new()
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeGeneric<TH>(string eventName) where TH : GenericEventHandler
        {
            throw new NotImplementedException();
            //review : http://masstransit-project.com/MassTransit/advanced/courier/subscriptions.html
        }

        public void Unsubscribe<T, TH>()
            where T : Event, new()
            where TH : Contracts.EventHandler<T>, new()
        {
            throw new NotImplementedException();
        }
    }
}