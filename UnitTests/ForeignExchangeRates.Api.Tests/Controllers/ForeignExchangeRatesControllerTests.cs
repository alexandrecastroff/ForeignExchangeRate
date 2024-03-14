namespace ForeignExchangeRates.Api.Tests.Controllers
{
    using CrossInfrastructure.Services;
    using ForeignExchangeRates.Controllers;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Net;
    using System.Web.Mvc;
    using Xunit;

    public class ForeignExchangeRatesControllerTests
    {
        private ForeignExchangeRatesController controller;

        public ForeignExchangeRatesControllerTests()
        {
            var foreignExchangeRatesServiceMock = new Mock<IForeignExchangeRatesService>();
            var loggerMock = new Mock<ILogger<ForeignExchangeRatesController>>();

            this.controller = new ForeignExchangeRatesController(foreignExchangeRatesServiceMock.Object, loggerMock.Object);
        }

        [Theory]
        [InlineData("sdk", "EUR")]
        [InlineData("EUR", "sdf")]
        [InlineData("sdf", "sds")]
        [InlineData("d", "EUR")]
        [InlineData("EUR", "ff")]
        public async Task GetForeignExchangeRate_InvalidInputCurrencies_BadRequestWasThrown(string fromCurrency, string toCurrency)
        {
            // Act
            var result = await this.controller.Get(fromCurrency, toCurrency).ConfigureAwait(false) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData("sdk", "EUR")]
        [InlineData("EUR", "sdf")]
        [InlineData("sdf", "sds")]
        [InlineData("d", "EUR")]
        [InlineData("EUR", "ff")]
        public async Task DeleteForeignExchangeRate_InvalidInputCurrencies_BadRequestWasThrown(string fromCurrency, string toCurrency)
        {
            // Act
            var result = await this.controller.Delete(fromCurrency, toCurrency).ConfigureAwait(false) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task PatchForeignExchangeRate_NullModel_BadRequestWasThrown()
        {
            // Act
            var result = await this.controller.Patch(null).ConfigureAwait(false) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
