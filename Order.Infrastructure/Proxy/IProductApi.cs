using MongoDB.Bson;
using Order.Domain.Enities;
using Order.Domain.ValueObjects;
using RestEase;

namespace Order.Infrastructure.Proxy
{
    public interface IProductApi
    {
        //mayby add reservation status
        //propably better use another object without price and name
        [Put("Product/Reservation")]
        Task<bool> ReserveProducts([Body] List<Product> products, CancellationToken cancellationToken = default);

    }
}
