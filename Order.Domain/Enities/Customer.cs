using BuildingBlocks.Domain;
using MongoDB.Bson;
using Order.Domain.ValueObjects;

namespace Order.Domain.Enities
{
    public class Customer : Entity
    {
        public Name Name { get; set; }
        public Adress Adress { get; set; }
        public CustomerDetails MapToCustomerDetails() => new CustomerDetails(Id, Name, Adress);
    }
}
