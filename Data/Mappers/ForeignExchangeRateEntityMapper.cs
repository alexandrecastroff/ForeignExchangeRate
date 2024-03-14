namespace Data.Mappers
{
    using Application.Domain.Domain;
    using Data.Entities;

    public static class ForeignExchangeRateEntityMapper
    {
        public static ForeignExchangeRateEntity ToEntity(this ForeignExchangeRate foreignExchangeRate)
        {
            return new ForeignExchangeRateEntity
            {
                FromCurrency = foreignExchangeRate.FromCurrency,
                ToCurrency = foreignExchangeRate.ToCurrency,
                Bid = foreignExchangeRate.Bid,
                Ask = foreignExchangeRate.Ask,
            };
        }

        public static ForeignExchangeRate ToDomain(this ForeignExchangeRateEntity foreignExchangeRateEntity)
        {
            return new ForeignExchangeRate
            {
                FromCurrency = foreignExchangeRateEntity.FromCurrency,
                ToCurrency = foreignExchangeRateEntity.ToCurrency,
                Bid = foreignExchangeRateEntity.Bid,
                Ask = foreignExchangeRateEntity.Ask,
            };
        }
    }
}
