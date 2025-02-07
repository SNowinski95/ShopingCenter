using MongoDB.Bson;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Commends.CreateOrder
{
    public record OrderCreateDto(ObjectId customerId, OrderDetails orderDetails, List<Product> products);
}
