using MongoDB.Bson;
using Order.Domain.Enities;
using Order.Infrastructure.Proxy;
using RestEase;

namespace Order.Mock.Infrastructure
{
    public class UserApi : IUserApi
    {
        public async Task<Customer> GetUser( string id, CancellationToken cancellationToken = default)
        {
            return new Customer
            {
                Id = id,
                Adress = new Domain.ValueObjects.Adress("44-194", "Knurów", "Poland", "zxc", 1, 1)
            };
        }
    }
}
