using MongoDB.Bson;
using Order.Domain.Enities;
using RestEase;
namespace Order.Infrastructure.Proxy
{
    public interface IUserApi
    {
        [Get("users/{id}")]
        Task<Customer> GetUser([Path] ObjectId id);
    }
}
