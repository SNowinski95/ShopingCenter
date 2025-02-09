using BuildingBlocks.Application.Commends;

namespace Application.Orders.PayForOrder;

public class PayForOrderCommand : ICommand<bool>
{
    public PayForOrderCommand(Guid customerId, decimal amount, Guid orderId)
    {
        CustomerId = customerId;
        Amount = amount;
        OrderId = orderId;
    }

    public Guid CustomerId { get; }
    
    public decimal Amount { get; }
    
    public Guid OrderId { get; }
}