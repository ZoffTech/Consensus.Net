using consensus.net.bus;
using consensus.net.bus.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace consensus.net.core
{
    public static class CoreExtensions
    {
        private static IConfiguration Configuration;

        public static void AddConsensus(this IServiceCollection services, Action<IConsensusOptions> config = null)
        {
            Configuration = services.BuildServiceProvider().GetService(typeof(IConfiguration)) as IConfiguration;

            AppData.HostName = Configuration["HostName"];

            IConsensusOptions options = new ConsensusOptions();

            if (config != null)
                config(options);

          


            services.AddSingleton< IConsensusOptions>(options);
            foreach (var item in options.EventBusOptions.Consumers)
            {
                services.AddTransient(item.GetType());
            }
         

            services.AddSingleton<IEventBus, EventBus>();

            services.AddHealthChecks();

            services.BuildServiceProvider();
        }
    }
}