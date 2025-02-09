using BuildingBlocks.Application.Queries;
using Order.Domain;

namespace Order.Application.Query.GetOrder
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            ArgumentNullException.ThrowIfNull(orderRepository,nameof(orderRepository));
            _orderRepository = orderRepository;
        }

        

        public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken cancellationToken = default)
        {
           var order = await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken);
            return OrderDto.Map(order);
        }
    }
}
