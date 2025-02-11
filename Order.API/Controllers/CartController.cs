using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Order.Domain;
using Order.Domain.Enities;
using Order.Domain.ValueObjects;
using ZiggyCreatures.Caching.Fusion;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        //replace with redis
        private readonly IShopingCartRepository _shopingCartRepository;
        public CartController(IShopingCartRepository shopingCartRepository)
        {
            ArgumentNullException.ThrowIfNull(shopingCartRepository, nameof(shopingCartRepository));
            _shopingCartRepository = shopingCartRepository;
        }

        [HttpPost("{customerId}")]
        public async Task<ActionResult<ShopingCart>> UpdateCart(string customerId, [FromBody] Product product, CancellationToken cancellationToken)
        {
            return await _shopingCartRepository.Add(product, customerId, cancellationToken);
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult<ShopingCart>> GetCart(string customerId, CancellationToken cancellationToken)
        {
            return await _shopingCartRepository.Get(customerId, cancellationToken);
        }
    }
}
