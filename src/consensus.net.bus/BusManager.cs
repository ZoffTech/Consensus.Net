using consensus.net.bus.Contracts;
using consensus.net.bus.Contracts.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace consensus.net.bus
{
    /// <summary>
    /// This class is the fundation to handle subscription to Messages sent by services across the ecosystem.
    ///
    /// </summary>

    public class BusManager : IDisposable, IBusManager
    {
        public bool Started;

        protected ILogger<BusManager> _logger;

        protected BusManagerOptions _options;

        public BusManager(BusManagerOptions options, ILogger<BusManager> logger)
        {
            _options = options;
            _logger = logger;
            AppData.Logger = logger;
            Start();
        }

        protected IRabbitMqBusFactoryConfigurator _busConfigurator { get; private set; }

        protected IBusControl _busControl { get; private set; }

        protected IRabbitMqHost _host { get; private set; }

        protected bool disposed { get; private set; }

        public void AddConsumer<T>(T Consumer, string queueName) where T : class, IConsumer, new()
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

        public Task Publish<T>(T message) where T : class
        {
            var submit = _busControl.Publish<T>(message);
            submit.Wait();
            return submit;
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
                    var host = sbc.Host(new Uri(_options.HostName), c =>
                    {
                        c.Password("guest");
                        c.Username("guest");
                    });
                  
                        sbc.ReceiveEndpoint(host, e =>
                    {
                        foreach (var c in _options.Consumers)
                        {
                            if (c is PingRequestConsumer)
                                e.Consumer<PingRequestConsumer>();
                            if (c is PingResponseConsumer)
                                e.Consumer<PingResponseConsumer>();
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
    }
}