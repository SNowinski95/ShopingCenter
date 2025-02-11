using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Order.Domain;
using Order.Domain.Enities;
using Order.Infrastructure.Configuration;
using Order.Infrastructure.Domain;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace Order.Infrastructure.ServiceExtension
{
    public static class DbExtensions
    {
        public static void AddMongoDBCollections(this IServiceCollection services, IConfiguration configuration)
        {

            var config = configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
            ArgumentNullException.ThrowIfNull(config);
            services.AddScoped(provider =>
            {
                var mongoClient = new MongoClient(
                    config.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    config.DatabaseName);

                return mongoDatabase.GetCollection<Order.Domain.Enities.Order>(
                    config.CollectionName);
            });
        }
        public static void AddReposiotry(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderReposiotry>();
            services.AddScoped<IShopingCartRepository, ShopingCartCacheRepository>();
        }
        public static void AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(CacheConfig)).Get<CacheConfig>();
            ArgumentNullException.ThrowIfNull(config);
            services.AddFusionCache()
                .WithDefaultEntryOptions(options => options.Duration = TimeSpan.FromMinutes(config.Duration))
                .WithSerializer(new FusionCacheSystemTextJsonSerializer())
                .WithDistributedCache(new RedisCache(new RedisCacheOptions { Configuration = config.Host}));
        }
    }
}
