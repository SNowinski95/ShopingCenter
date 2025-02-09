using BuildingBlocks.Application.Commends;
using MongoDB.Bson;
using Order.Application.Commends.CreateOrder;
using Order.Domain.ValueObjects;

namespace Order.Application.Commends.Create;

public class CreateOrderCommand(OrderCreateDto orderCreateDto) : ICommand<CreateOrderResult>
{
    public OrderDetails OrderDetails { get; } = orderCreateDto.orderDetails;
    public string CustomerId { get; } = orderCreateDto.customerId;

}