using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Order.Application.Commends.Create;
using Order.Application.Commends.CreateOrder;
using Order.Application.Commends.PayForOrder;
using Order.Application.Query.GetOrder;
using Order.Domain;
using Order.Domain.ValueObjects;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        //TO DO: add shoping card provided by cach propably preview of ordre, mayby in separte controller
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
