using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Configuration;
using Order.Infrastructure.Proxy;
using RestEase.HttpClientFactory;

namespace Order.Infrastructure.ServiceExtension
{
    public static class ProxyExtensions
    {
        private const string UserApiHost = "UserApi";
        private const string ProductApiHost = "UserApi";
        public static void AddProxy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRestEaseClient<IUserApi>(configuration.GetValue<string>(UserApiHost));
            services.AddRestEaseClient<IProductApi>(configuration.GetValue<string>(ProductApiHost));
        }
    }
}
