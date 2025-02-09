namespace Order.Domain.ValueObjects
{
    public record Adress(string ZipCode,  string City,  string Coutry, string Street, int HouseNumber, int? ApartmentNumber);
}
