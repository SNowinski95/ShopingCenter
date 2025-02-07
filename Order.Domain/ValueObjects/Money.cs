namespace Order.Domain.ValueObjects
{
    public record Money(decimal Value, string CurrencyCode)
    {
        public static Money Of(decimal value, string currencyCode)
        {
            return new Money(value, currencyCode);
        }
        public static Money operator *(int left, Money right) => Of(left * right.Value, right.CurrencyCode);



    }
    public static class MoneyExtensions
    {
        public static Money Sum(this IEnumerable<Money> moneyCollection)
        {

            if (moneyCollection == null || !moneyCollection.Any())
            {
                return Money.Of(0, null);
            }
            var currencyCode = moneyCollection.First().CurrencyCode;
            if (moneyCollection.Any(n => n.CurrencyCode != currencyCode))
            {
                throw new ArgumentException("Currencies don't match.");
            }
            decimal res = 0;
            foreach (var item in moneyCollection)
            {
                res += item.Value;
            }
            return Money.Of(res, currencyCode);
        }
    }
}
