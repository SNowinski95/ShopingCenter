using BuildingBlocks.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using Order.Domain.ValueObjects;
using System;

namespace Order.Domain.Enities
{
    public class Order : Entity
    {
        public DateTimeOffset CreatedTime { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public Payment Payment { get; set; }

        public static Order Create(DateTimeOffset currentTime, CustomerDetails customerDetails, List<Product> products) 
        {
            return new Order
            {
                Id = ObjectId.GenerateNewId(),
                CreatedTime = currentTime,
                Payment = new Payment(PaymentStatus.WaitingForReservation),
                CustomerDetails = customerDetails,
                OrderDetails = new OrderDetails(products)
            };
        }
    }
}
