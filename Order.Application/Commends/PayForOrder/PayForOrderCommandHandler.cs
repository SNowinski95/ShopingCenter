using Application.Orders.PayForOrder;
using BuildingBlocks.Application.Commends;
using MediatR;

namespace Application.Orders.PayForOrder;

public class PayForOrderCommandHandler : ICommandHandler<PayForOrderCommand, bool>
{

    public PayForOrderCommandHandler()
    {
    }

    public async Task<bool> Handle(PayForOrderCommand command, CancellationToken cancellationToken = default)
    {
        return true;
    }
}