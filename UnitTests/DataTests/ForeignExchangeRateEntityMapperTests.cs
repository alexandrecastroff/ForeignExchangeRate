using Application.Domain.Domain;
using Data.Entities;
using Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTests
{
    public class ForeignExchangeRateEntityMapperTests
    {
        [Fact]
        public void ToDomain_ValidEntity_ReturnMappedObject()
        {
            // Arrange
            var foreignExchangeRateEntity = new ForeignExchangeRateEntity
            {
                FromCurrency = "EUR",
                ToCurrency = "USD",
                Bid = 0.5M,
                Ask = 0.2M,
            };

            // Act
            var result = foreignExchangeRateEntity.ToDomain();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(foreignExchangeRateEntity.FromCurrency, result.FromCurrency);
            Assert.Equal(foreignExchangeRateEntity.ToCurrency, result.ToCurrency);
            Assert.Equal(foreignExchangeRateEntity.Bid, result.Bid);
            Assert.Equal(foreignExchangeRateEntity.Ask, result.Ask);
        }

        [Fact]
        public void ToEntity_ValidDomain_ReturnMappedObject()
        {
            // Arrange
            var foreignExchangeRate = new ForeignExchangeRate
            {
                FromCurrency = "EUR",
                ToCurrency = "USD",
                Bid = 0.5M,
                Ask = 0.2M,
            };

            // Act
            var result = foreignExchangeRate.ToEntity();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(foreignExchangeRate.FromCurrency, result.FromCurrency);
            Assert.Equal(foreignExchangeRate.ToCurrency, result.ToCurrency);
            Assert.Equal(foreignExchangeRate.Bid, result.Bid);
            Assert.Equal(foreignExchangeRate.Ask, result.Ask);
        }
    }
}
