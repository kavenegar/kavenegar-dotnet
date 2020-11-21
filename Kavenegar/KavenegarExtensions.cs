using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace Kavenegar
{
    public static class KavenegarExtensions
    {
        /// <summary>
        /// Adds Kavenegar Services to <paramref name="services"/>
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configure">Configure Kavenegar settings</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection AddKavenegar(this IServiceCollection services, Action<KavenegarSettings> configure)
        {
            services.Configure(configure);

            services.AddScoped<IKavenegarApi, KavenegarApi>();

            services.AddHttpClient(Constants.HttpClientName)
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(i * 200)));

            return services;
        }
    }
}
