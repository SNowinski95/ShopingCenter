
using MongoDB.Bson;
using Order.Domain.ValueObjects;

namespace Order.Domain
{
    public interface IOrderRepository
    {
        Task AddAsync(Enities.Order order, CancellationToken cancellationToken = default);
        Task UdateStatusAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken = default);

        ValueTask<Enities.Order?> GetByIdAsync(string orderId, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Enities.Order>> GetFiteredAsync(string customerId, PaymentStatus? paymentStatus = null, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    }
}
