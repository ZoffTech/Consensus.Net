using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consensus.net.bus.Contracts.Consumers;
using consensus.net.core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace consensus.net.keyvalue.service
{
    public class Startup
    {
        public Startup(IConfiguration configuration,ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IConfiguration _configuration { get; }

        private readonly ILogger<Startup> _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddConsensus(config =>
            {
                config.EventBusOptions = new bus.EventBusOptions
                {
                    Consumers = new List<MassTransit.IConsumer>
                    {
                        new HeartbeatRequestHandler(),
                    }
                };
            });
         
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseMvc();
            app.EnableConsensus();

        }
    }
}
