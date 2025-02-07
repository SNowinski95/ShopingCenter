using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects
{
    public record OrderDetails(List<Product> Products)
    {
        public Money GetFullPrice()
        {
            if (Products == null || Products.Count == 0) return new Money(0, null);
            return Products.Select(n=> n.Quantity * n.price).Sum();
                    
        }
        

    }
   

}
