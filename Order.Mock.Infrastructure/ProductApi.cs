using Order.Domain.ValueObjects;
using Order.Infrastructure.Proxy;
using RestEase;

namespace Order.Mock.Infrastructure
{
    public class ProductApi : IProductApi
    {
        public async Task<bool> ReserveProducts([Body] List<Product> products, CancellationToken cancellationToken = default)
        {
            //return 200
            return true;
            
        }
    }
}
