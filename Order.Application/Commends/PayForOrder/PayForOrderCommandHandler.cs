using Bottega.PhotoStock.BuildingBlocks.Application.Commands;
using Bottega.PhotoStock.BuildingBlocks.Application.Database;
using Bottega.PhotoStock.Sales.Domain.Orders;
using MediatR;

namespace Bottega.PhotoStock.Sales.Application.Orders.PayForOrder;

public class PayForOrderCommandHandler : ICommandHandler<PayForOrderCommand>
{
    private readonly IPaymentService _paymentService;

    private readonly IOrderRepository _orderRepository;

    public PayForOrderCommandHandler(IPaymentService paymentService, IOrderRepository orderRepository)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(PayForOrderCommand command, CancellationToken cancellationToken)
    {
        await _paymentService.Pay(command.CustomerId, command.Amount);

        var order = await _orderRepository.GetById(command.OrderId);

        order!.MarkAsPaid();

        return Unit.Value;
    }
}