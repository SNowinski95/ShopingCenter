using MongoDB.Bson;
using Order.Domain.Enities;
using Order.Domain.ValueObjects;
using RestEase;
namespace Order.Infrastructure.Proxy
{
    public interface IUserApi
    {
        [Get("users/{id}")]
        Task<Customer> GetUser([Path] string id, CancellationToken cancellationToken = default);
    }
}
