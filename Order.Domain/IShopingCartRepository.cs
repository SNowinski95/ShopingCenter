using Order.Domain.Enities;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public interface IShopingCartRepository
    {
        Task<ShopingCart> Add(Product product, string customerId, CancellationToken cancellationToken = default);
        Task<bool> Clear(string customerId, CancellationToken cancellationToken = default);
        Task<ShopingCart> Get(string customerId, CancellationToken cancellationToken = default);
    }
}
