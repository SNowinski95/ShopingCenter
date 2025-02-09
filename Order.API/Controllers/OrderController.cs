using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Order.Application.Commends.Create;
using Order.Application.Commends.CreateOrder;
using Order.Application.Commends.PayForOrder;
using Order.Application.Query.GetOrder;
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
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> Get(string id, CancellationToken cancellationToken)
        {
            //TO Wrap for some fail result or support exception inside
            var result = await _mediator.Send(new GetOrderQuery(id), cancellationToken);
            //insted of null better some object with status not found
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpGet("~/search")]
        public async Task<ActionResult<OrderDto>> Search([FromQuery] string CustomerId, [FromQuery] PaymentStatus paymentStatus, [FromQuery]DateTime from,[FromQuery]DateTime to, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
