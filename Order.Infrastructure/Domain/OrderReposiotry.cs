using MongoDB.Bson;
using MongoDB.Driver;
using Order.Domain;
using Order.Domain.ValueObjects;
using System.Threading;

namespace Order.Infrastructure.Domain
{
    public class OrderReposiotry : IOrderRepository
    {
        private readonly IMongoCollection<Order.Domain.Enities.Order> _collection;

        public OrderReposiotry(IMongoCollection<Order.Domain.Enities.Order> collection)
        {
            _collection = collection;
        }

        //Wrap exception to all
        public async Task Add(Order.Domain.Enities.Order order, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(order, cancellationToken: cancellationToken);
        }

        public async ValueTask<Order.Domain.Enities.Order> GetById(ObjectId orderId, CancellationToken cancellationToken)
        {
            return await _collection.Find(n => n.Id == orderId).SingleAsync(cancellationToken);
        }

        public async Task UdateStatus(ObjectId id, PaymentStatus paymentStatus, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(n => n.Id == id, Builders<Order.Domain.Enities.Order>.Update.Set(n=>n.Payment.PaymentStatus, paymentStatus));
        }
        //move to filter object
        public async ValueTask<IEnumerable<Order.Domain.Enities.Order>> GetFitered(ObjectId id,PaymentStatus? paymentStatus, DateTime? from, DateTime? to ,CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<Order.Domain.Enities.Order>.Filter;
            var filter = filterBuilder.Eq(n => n.Id, id);
            if (paymentStatus != null) filter= filter & filterBuilder.Eq(n => n.Payment.PaymentStatus, paymentStatus);
            if(from != null && to != null)
            {
                filter = filter & filterBuilder.Gte(x => x.CreatedTime, new DateTimeOffset(from.Value)) 
                    & filterBuilder.Lt(x => x.CreatedTime, new DateTimeOffset(to.Value));
            }
            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }
    }
}
