using Order.Domain.ValueObjects;

namespace Order.Application.Query.GetOrder
{
    public class OrderDto
    {
        public DateTimeOffset Time { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public Payment Payment { get; set; }
    }
}
