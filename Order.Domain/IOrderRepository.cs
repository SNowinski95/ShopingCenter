
using MongoDB.Bson;
using Order.Domain.ValueObjects;

namespace Order.Domain
{
    public interface IOrderRepository
    {
        Task Add(Enities.Order order,CancellationToken cancellationToken);
        Task UdateStatus(ObjectId id, PaymentStatus paymentStatus, CancellationToken cancellationToken);

        ValueTask<Enities.Order?> GetById(ObjectId orderId, CancellationToken cancellationToken);
        ValueTask<IEnumerable<Order.Domain.Enities.Order>> GetFitered(ObjectId id, PaymentStatus? paymentStatus, DateTime? from, DateTime? to, CancellationToken cancellationToken);
    }
}
