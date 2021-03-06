﻿using consensus.net.bus;
using consensus.net.bus.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace consensus.net.service_registry.HostedService
{
    public class PingScheduler : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IEventBus _bus;
        private readonly HeartbeatRequestEvent _request;
        private Timer _timer;

        public PingScheduler(IEventBus bus, ILogger<PingScheduler> logger)
        {
            _logger = logger;
            _bus = bus;
            _request = new HeartbeatRequestEvent(logger);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _bus.Publish(_request);
            _logger.LogInformation("Ping Sent");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ping Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}