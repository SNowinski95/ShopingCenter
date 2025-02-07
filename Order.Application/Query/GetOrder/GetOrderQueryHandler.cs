using BuildingBlocks.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Query.GetOrder
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {


        public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }
    }
}
