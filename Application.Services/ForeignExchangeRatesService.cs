namespace Application.Services
{
    using Application.Domain.Domain;
    using CrossInfrastructure.Gateways;
    using CrossInfrastructure.Kafka;
    using CrossInfrastructure.Mongo;
    using CrossInfrastructure.Services;
    using Microsoft.Extensions.Logging;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ForeignExchangeRatesService : IForeignExchangeRatesService
    {
        private IMongoRepository mongoRepository;
        private IExchangeRateGateway exchangeRateGateway;
        private readonly IForeignExchangeRateCreatedEventProducer eventProducer;
        private readonly ILogger<ForeignExchangeRatesService> _logger;

        public ForeignExchangeRatesService(
            IMongoRepository mongoRepository,
            IExchangeRateGateway exchangeRateGateway,
            IForeignExchangeRateCreatedEventProducer eventProducer,
            ILogger<ForeignExchangeRatesService> logger)
        {
            this.mongoRepository = mongoRepository;
            this.exchangeRateGateway = exchangeRateGateway;
            this.eventProducer = eventProducer;
            _logger = logger;
        }

        public async Task<ForeignExchangeRate> GetForeignExchangeRate(string fromCurrency, string toCurrency)
        {
            // Validate if present on db
            var exchangeRate = await this.mongoRepository.GetAsync(fromCurrency, toCurrency).ConfigureAwait(false);

            if (exchangeRate == null)
            {
                _logger.LogInformation($"{nameof(ForeignExchangeRatesService)}.{nameof(this.GetForeignExchangeRate)} - ForeignExchangeRate for pair {fromCurrency}|{toCurrency} not found on DB.");
                // get value from provider gateway
                exchangeRate = await this.exchangeRateGateway.GetForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);

                if (exchangeRate != null)
                {
                    // insert new rate on db
                    await this.mongoRepository.CreateAsync(exchangeRate).ConfigureAwait(false);

                    var message = JsonSerializer.Serialize(exchangeRate);
                    await this.eventProducer.ProduceAsync("ForeignExchangeRateUpdates", message).ConfigureAwait(false);

                    _logger.LogInformation($"{nameof(ForeignExchangeRatesService)}.{nameof(this.GetForeignExchangeRate)} - ForeignExchangeRate for pair {fromCurrency}|{toCurrency} inserted on DB.");
                }
            }

            return exchangeRate;
        }

        public async Task DeleteForeignExchangeRate(string fromCurrency, string toCurrency)
        {
            await this.mongoRepository.RemoveAsync(fromCurrency, toCurrency).ConfigureAwait(false);
        }

        public async Task UpdateForeignExchangeRate(ForeignExchangeRate foreignExchangeRate)
        {
            await this.mongoRepository.UpdateAsync(foreignExchangeRate.FromCurrency, foreignExchangeRate.ToCurrency, foreignExchangeRate).ConfigureAwait(false);
        }
    }
}
