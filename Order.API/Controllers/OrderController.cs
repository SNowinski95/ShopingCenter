using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Order.Application.Commends.Create;
using Order.Application.Commends.CreateOrder;
using Order.Application.Commends.PayForOrder;
using Order.Application.Query.GetOrder;
using Order.Domain;
using Order.Domain.ValueObjects;
using ZiggyCreatures.Caching.Fusion;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        //TO DO: add shoping card provided by cach propably preview of ordre, mayby in separte controller
        private readonly IMediator _mediator;
        private readonly IFusionCache _fusionCache;

        public OrderController(IMediator mediator, IFusionCache fusionCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _fusionCache = fusionCache ?? throw new ArgumentNullException(nameof(mediator));
        }
        //move to CartController and logic to comand
        [HttpPost("~/Cart")]
        public async Task<ActionResult<List<Product>>> UpdateCart([FromBody] Product product, ObjectId customerId, CancellationToken cancellationToken)
        {
            var currentCart = await _fusionCache.TryGetAsync<List<Product>>("currentOrder");
            if (currentCart.HasValue) 
            { 
                currentCart.Value.Add(product);
            }
            await _fusionCache.SetAsync($"currentOrder:{customerId.ToString()}", currentCart.Value, new FusionCacheEntryOptions { DistributedCacheDuration = new TimeSpan(1, 0, 0) });
            return currentCart.Value;
        }
        //move to CartController and logic to query
        [HttpGet("~/Cart")]
        public async Task<ActionResult<List<Product>>> GetCart(ObjectId customerId, CancellationToken cancellationToken)
        {
            var value = await _fusionCache.TryGetAsync<List<Product>>("currentOrder");
            return value.GetValueOrDefault();
        }
        [HttpGet("~/{id}")]
        public async Task<ActionResult<OrderDto>> Get(ObjectId id, CancellationToken cancellationToken)
        {
            //TO Wrap for some fail result or support exception inside
            var result = await _mediator.Send(new GetOrderQuery(id), cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpGet("~/search")]
        public async Task<ActionResult<OrderDto>> Search([FromQuery] ObjectId CustomerId, [FromQuery] PaymentStatus paymentStatus, [FromQuery]DateTime from,[FromQuery]DateTime to, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]OrderCreateDto orderCreate,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateOrderCommand(orderCreate), cancellationToken);
            return Ok(result);
        }
        
        [HttpPatch]
        public async Task<ActionResult> UpdatePaymentStatus([FromBody] OrderUpdateStatusDto orderUpdateStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
