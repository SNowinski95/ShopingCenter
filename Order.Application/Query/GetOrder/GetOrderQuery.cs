using BuildingBlocks.Application.Queries;
using MongoDB.Bson;

namespace Order.Application.Query.GetOrder
{
    public class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(ObjectId orderId)
        {
            OrderId = orderId;
        }

        public ObjectId OrderId { get; }
    }
}
