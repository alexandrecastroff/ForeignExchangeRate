namespace Application.Services.Tests
{
    using Application.Domain.Domain;
    using CrossInfrastructure.Gateways;
    using CrossInfrastructure.Mongo;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ForeignExchangeRatesServiceTests
    {
        private Mock<ILogger<ForeignExchangeRatesService>> loggerMock;
        private Mock<IExchangeRateGateway> exchangeRateGatewayMock;
        private Mock<IMongoRepository> mongoMock;
        private ForeignExchangeRatesService foreignExchangeRatesService;

        public ForeignExchangeRatesServiceTests()
        {
            loggerMock = new Mock<ILogger<ForeignExchangeRatesService>>();
            exchangeRateGatewayMock = new Mock<IExchangeRateGateway>();
            mongoMock = new Mock<IMongoRepository>();

            this.foreignExchangeRatesService = new ForeignExchangeRatesService(
                mongoMock.Object, exchangeRateGatewayMock.Object,
                loggerMock.Object);
        }

        [Fact]
        public async Task DeleteForeignExchangeRate_RepositoryWasCalled()
        {
            // Arrange
            var fromCurrency = "EUR";
            var toCurrency = "USD";

            // Act
            await this.foreignExchangeRatesService.DeleteForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);

            // Assert
            this.mongoMock.Verify(x => x.RemoveAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateForeignExchangeRate_RepositoryWasCalled()
        {
            // Arrange
            var fromCurrency = "EUR";
            var toCurrency = "USD";
            var model = new ForeignExchangeRate
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Bid = 1,
                Ask = 2,
            };

            // Act
            await this.foreignExchangeRatesService.UpdateForeignExchangeRate(model).ConfigureAwait(false);

            // Assert
            this.mongoMock.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ForeignExchangeRate>()), Times.Once);
        }

        [Fact]
        public async Task GetForeignExchangeRate_ValueOnRepository_ReturnsFromRepository()
        {
            // Arrange
            var fromCurrency = "EUR";
            var toCurrency = "USD";
            var model = new ForeignExchangeRate
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Bid = 1,
                Ask = 2,
            };

            this.mongoMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(model);

            // Act
            var result = await this.foreignExchangeRatesService.GetForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fromCurrency, result.FromCurrency);
            Assert.Equal(toCurrency, result.ToCurrency);
            Assert.Equal(model.Bid, result.Bid);
            Assert.Equal(model.Ask, result.Ask);

            this.exchangeRateGatewayMock.Verify(x => x.GetForeignExchangeRate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            this.mongoMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetForeignExchangeRate_ValueNotPresentOnRepository_ReturnsFromProviderAndSaveOnRepository()
        {
            // Arrange
            var fromCurrency = "EUR";
            var toCurrency = "USD";
            var model = new ForeignExchangeRate
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Bid = 1,
                Ask = 2,
            };

            this.mongoMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((ForeignExchangeRate)null);
            this.exchangeRateGatewayMock.Setup(x => x.GetForeignExchangeRate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(model);

            // Act
            var result = await this.foreignExchangeRatesService.GetForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fromCurrency, result.FromCurrency);
            Assert.Equal(toCurrency, result.ToCurrency);
            Assert.Equal(model.Bid, result.Bid);
            Assert.Equal(model.Ask, result.Ask);

            this.exchangeRateGatewayMock.Verify(x => x.GetForeignExchangeRate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            this.mongoMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            this.mongoMock.Verify(x => x.CreateAsync(It.IsAny<ForeignExchangeRate>()), Times.Once);
        }
    }
}
