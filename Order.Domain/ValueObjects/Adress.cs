namespace Order.Domain.ValueObjects
{
    public record Adress(string ZipCode, string Street, string City, string Coutry, int HouseNumber, int? ApartmentNumber);
}
