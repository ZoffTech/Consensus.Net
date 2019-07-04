using consensus.net.bus;
using consensus.net.bus.Contracts;
using consensus.net.bus.Contracts.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace consensus.net.core {
    public static class CoreExtensions {

        static IConfiguration Configuration;
        public static void AddConsensus (this IServiceCollection services,ConsensusOptions options=null) {

            Configuration = services.BuildServiceProvider ().GetService (typeof (IConfiguration)) as IConfiguration;

            AppData.HostName = Configuration["HostName"];

            services.AddSingleton<BusManagerOptions>();

            services.AddSingleton<IBusManager,BusManager>();
          
            services.AddHealthChecks ();


            
        }

    }
}

