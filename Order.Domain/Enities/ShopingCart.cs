using BuildingBlocks.Domain;
using Order.Domain.ValueObjects;

namespace Order.Domain.Enities
{
    //create CashEntity??
    public class ShopingCart : Entity
    {
        public const string CacheKey = "currentOrder";
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
