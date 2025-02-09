using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using Order.Domain.ValueObjects;
using ZiggyCreatures.Caching.Fusion;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        //replace with redis
        private readonly IMemoryCache _cache;
        private const string CartName = "currentOrder";
        public CartController(IMemoryCache cache)
        {
            ArgumentNullException.ThrowIfNull(cache, nameof(cache));
        }
        
        [HttpPost("{customerId}")]
        public async Task<ActionResult<List<Product>>> UpdateCart(string customerId, [FromBody] Product product, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue<List<Product>>(CartName, out var currentCart))
            {
                currentCart.Add(product);
                _cache.Set($"{CartName}:{customerId.ToString()}", currentCart);
                return currentCart;
            }
            var cacheEntry = _cache.CreateEntry($"{CartName}:{customerId.ToString()}");
            currentCart = new List<Product>() { product };
            cacheEntry.SetValue(currentCart);
            return currentCart;
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Product>>> GetCart(string customerId, CancellationToken cancellationToken)
        {
            return _cache.TryGetValue<List<Product>>(CartName, out var currentCart) ? currentCart : new List<Product>();
        }
    }
}
