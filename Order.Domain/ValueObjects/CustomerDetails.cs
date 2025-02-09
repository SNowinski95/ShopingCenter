
using MongoDB.Bson;

namespace Order.Domain.ValueObjects
{
    public record CustomerDetails(string CustomerId, Name Name, Adress Adress);
}
