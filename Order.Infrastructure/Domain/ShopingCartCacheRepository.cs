using Order.Domain;
using Order.Domain.Enities;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace Order.Infrastructure.Domain
{
    public class ShopingCartCacheRepository : IShopingCartRepository
    {
        private readonly IFusionCache _fusionCache;
        private const string CacheName = "shopingCart";
        public ShopingCartCacheRepository(IFusionCache fusionCache) 
        {
            ArgumentNullException.ThrowIfNull(fusionCache, nameof(fusionCache));
            _fusionCache = fusionCache;
        }
        public async Task<ShopingCart> Add(Product product, string customerId, CancellationToken cancellationToken = default)
        {
            var cart = await Get(customerId, cancellationToken);
            cart.Products.Add(product);
            await _fusionCache.SetAsync(GetCacheName(customerId), cart, token: cancellationToken);
            return cart;
        }

        public async Task<bool> Clear(string customerId, CancellationToken cancellationToken = default)
        {
            await _fusionCache.RemoveAsync(GetCacheName(customerId), token: cancellationToken);
            return true;
        }

        public async Task<ShopingCart> Get(string customerId, CancellationToken cancellationToken = default)
        {
            return await _fusionCache.GetOrDefaultAsync<ShopingCart>(key: GetCacheName(customerId), token: cancellationToken) ?? new ShopingCart();
            
        }
        private string GetCacheName(string customerId) => $"{CacheName}:{customerId}";
    }
    
}
