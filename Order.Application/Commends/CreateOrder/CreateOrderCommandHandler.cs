﻿using BuildingBlocks.Application.Commends;
using MongoDB.Bson;
using Order.Domain;
using Order.Domain.ValueObjects;
using Order.Infrastructure.Proxy;
using System;

namespace Order.Application.Commends.Create;

public sealed class CreateOrderCommandHandler
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    //this is best practis for me to inject 
    //in other place i make it in faster but not proper way
    private readonly IOrderRepository _orderRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUserApi _userApi;
    private readonly IProductApi _productApi;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, TimeProvider timeProvider, IUserApi userApi, IProductApi productApi)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        _userApi = userApi ?? throw new ArgumentNullException(nameof(userApi));
        _productApi = productApi ?? throw new ArgumentNullException(nameof(productApi));
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //propably better way is just take id and quantity of products after that get price from productAPI
        //TODO wrap operation on API
        var user = await _userApi.GetUser(command.CustomerId);
        var customerDetails = new CustomerDetails(command.CustomerId,user.Name, user.Adress);
        //check by product API if price are correct
        var order = Domain.Enities.Order.Create(_timeProvider.GetUtcNow(), customerDetails, command.Products);
        //TODO wrap operation on repo
        await _orderRepository.Add(order,cancellationToken);
        await _productApi.ReserveProducts(command.Products);
        return new CreateOrderResult(ResultStatus.Success);

    }

}