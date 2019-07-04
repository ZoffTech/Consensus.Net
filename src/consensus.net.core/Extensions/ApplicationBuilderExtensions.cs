using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace consensus.net.core {
    public static class ApplicationBuilderExtensions {
        public static void EnableConsensus (this IApplicationBuilder app) {

            //Basic Healthcheck functonality.
            app.UseHealthChecks ("/live", new HealthCheckOptions {
                Predicate = (_) => true
            });

            app.UseHealthChecks ("/hc", new HealthCheckOptions {
                Predicate = (_) => true
            });
        }
    }

}