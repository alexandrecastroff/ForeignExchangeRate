using Application.Domain.Domain;
using CrossInfrastructure.Gateways;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace Application.Gateways.Implementations
{
    public class ExchangeRateGateway : IExchangeRateGateway
    {
        private readonly ILogger<ExchangeRateGateway> _logger;

        public ExchangeRateGateway(ILogger<ExchangeRateGateway> logger)
        {
            _logger = logger;
        }

        public async Task<ForeignExchangeRate> GetForeignExchangeRate(string fromCurrency, string toCurrency)
        {
            try
            {
                var result = this.GetForeignExchangeRateFromProvider(fromCurrency, toCurrency);
                _logger.LogInformation($"{nameof(ExchangeRateGateway)}.{nameof(this.GetForeignExchangeRate)} - ForeignExchangeRate for pair {fromCurrency}|{toCurrency} retrieved from external provider.");

                return result;
            }
            catch (Exception ex)
            {
                var message = $"{nameof(ExchangeRateGateway)}.{nameof(this.GetForeignExchangeRate)} - Error calling external provider for foreignExchangeRate for pair {fromCurrency}|{toCurrency}.";
                _logger.LogError(ex, message);

                throw new ApplicationException(message);
            }
        }

        private ForeignExchangeRate GetForeignExchangeRateFromProvider(string fromCurrency, string toCurrency)
        {
            var apiKey = "0MVBARUUBO344TXH";
            var QUERY_URL = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrency.ToUpperInvariant()}&to_currency={toCurrency.ToUpperInvariant()}&apikey={apiKey}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                var currencyExchangeRate = JsonConvert.DeserializeObject<CurrencyExchangeRate>(client.DownloadString(queryUri));

                return new ForeignExchangeRate
                {
                    FromCurrency = fromCurrency,
                    ToCurrency = toCurrency,
                    Bid = decimal.Parse(currencyExchangeRate.MetaData.BidPrice),
                    Ask = decimal.Parse(currencyExchangeRate.MetaData.AskPrice),
                };
            }
        }
    }
}
