namespace ForeignExchangeRates.Api.Mappers
{
    using Application.Domain.Domain;
    using ForeignExchangeRates.Api.Models;

    public static class ForeignExchangeRateViewModelMapper
    {
        public static ForeignExchangeRateViewModel ToViewModel(this ForeignExchangeRate foreignExchangeRate)
        {
            return new ForeignExchangeRateViewModel
            {
                FromCurrency = foreignExchangeRate.FromCurrency,
                ToCurrency = foreignExchangeRate.ToCurrency,
                Bid = foreignExchangeRate.Bid,
                Ask = foreignExchangeRate.Ask,
            };
        }

        public static ForeignExchangeRate ToDomain(this ForeignExchangeRateViewModel foreignExchangeRateViewModel)
        {
            return new ForeignExchangeRate
            {
                FromCurrency = foreignExchangeRateViewModel.FromCurrency,
                ToCurrency = foreignExchangeRateViewModel.ToCurrency,
                Bid = foreignExchangeRateViewModel.Bid,
                Ask = foreignExchangeRateViewModel.Ask,
            };
        }
    }
}
