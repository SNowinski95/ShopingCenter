using BuildingBlocks.Infrastructure.Queue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.JobScheduling;

namespace Order.Infrastructure.ServiceExtension
{
    public static class QueueExtensions
    {
        public static void AddConsumer<T>(this IServiceCollection services, IConfiguration configuration) where T : QueueConfig
        {
            var config = configuration.GetSection(typeof(T).Name).Get<T>();
            ArgumentNullException.ThrowIfNull(config);
            services.AddSingleton<IConsumer>(_ => new Consumer(config));
        }

    }
}
