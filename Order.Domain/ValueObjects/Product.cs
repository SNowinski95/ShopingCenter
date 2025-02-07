using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects
{
    public record Product(ObjectId Id, int Quantity, Money price);
}
