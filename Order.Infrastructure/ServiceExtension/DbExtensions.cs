using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Order.Infrastructure.Configuration;

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
    }
}
