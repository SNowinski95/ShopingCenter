using Order.Domain.ValueObjects;

namespace Order.Application.Query.GetOrder
{
    public class OrderDto
    {
        public DateTimeOffset CreatedTime { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public Payment Payment { get; set; }

        public static OrderDto Map(Domain.Enities.Order order)
        {
            if (order == null) return null;
            return new OrderDto
            {
                CustomerDetails = order.CustomerDetails,
                Payment = order.Payment,
                OrderDetails = order.OrderDetails,
                CreatedTime = order.CreatedTime
            };
        }

    }
}
