using Bottega.PhotoStock.BuildingBlocks.Application.Commands;

namespace Bottega.PhotoStock.Sales.Application.Orders.PayForOrder;

public class PayForOrderCommand : CommandBase, IEnqueueableCommand
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