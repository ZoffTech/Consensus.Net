using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;


namespace consensus.net.core {
    public static class CoreExtensions {

        static IConfiguration Configuration;
        public static void AddConsensus (this IServiceCollection services) {

            Configuration = services.BuildServiceProvider ().GetService (typeof (IConfiguration)) as IConfiguration;

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v2", new Info { Title = $"{Configuration["HOSTNAME"]}", Version = "v2" });
            });

            services.AddHealthChecks ();
        }

        public static void EnableConsensus (this IApplicationBuilder app, IConfiguration configuration) {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v2/swagger.json", $"{Configuration["TITLE"]}");
            });

            //Basic Healthcheck functonality.
            app.UseHealthChecks ("/liveness", new HealthCheckOptions {
                Predicate = (_) => true
            });

            app.UseHealthChecks ("/readiness", new HealthCheckOptions {
                Predicate = (_) => true
            });
        }
    }

}