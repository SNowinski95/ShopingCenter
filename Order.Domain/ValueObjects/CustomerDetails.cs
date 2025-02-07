
using MongoDB.Bson;

namespace Order.Domain.ValueObjects
{
    public record CustomerDetails(ObjectId CustomerId, Name Name, Adress Adress);
}
