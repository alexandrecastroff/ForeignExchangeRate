namespace Application.Gateways.Tests
{
    using Application.Gateways.Implementations;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ExchangeRateGatewayTests
    {
        private Mock<ILogger<ExchangeRateGateway>> loggerMock;
        private ExchangeRateGateway exchangeRateGateway;

        public ExchangeRateGatewayTests()
        {
            this.loggerMock = new Mock<ILogger<ExchangeRateGateway>>();
            this.exchangeRateGateway = new ExchangeRateGateway(this.loggerMock.Object);
        }

        [Fact]

    }
}
