using BuildingBlocks.Application.Queries;
using MongoDB.Bson;

namespace Order.Application.Query.GetOrder
{
    public class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; }
    }
}
