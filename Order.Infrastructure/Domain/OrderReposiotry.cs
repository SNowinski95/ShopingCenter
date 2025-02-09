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
        public async Task AddAsync(Order.Domain.Enities.Order order, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(order, cancellationToken: cancellationToken);
        }

        public async ValueTask<Order.Domain.Enities.Order> GetByIdAsync(string orderId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(n => n.Id == orderId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task UdateStatusAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken = default)
        {
            await _collection.UpdateOneAsync(n => n.Id == id, Builders<Order.Domain.Enities.Order>.Update.Set(n=>n.Payment.PaymentStatus, paymentStatus));
        }
        //move to filter object
        public async ValueTask<IEnumerable<Order.Domain.Enities.Order>> GetFiteredAsync(string customerId,PaymentStatus? paymentStatus, DateTime? from, DateTime? to ,CancellationToken cancellationToken = default)
        {
            var filterBuilder = Builders<Order.Domain.Enities.Order>.Filter;
            var filter = filterBuilder.Eq(n => n.CustomerDetails.CustomerId, customerId);
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
