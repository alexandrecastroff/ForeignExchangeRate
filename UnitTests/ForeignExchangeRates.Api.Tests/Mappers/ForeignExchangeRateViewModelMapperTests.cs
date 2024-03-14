using Application.Domain.Domain;
using ForeignExchangeRates.Api.Mappers;
using ForeignExchangeRates.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForeignExchangeRates.Api.Tests.Mappers
{
    public class ForeignExchangeRateViewModelMapperTests
    {
        [Fact]
        public void ToDomain_ValidViewModel_ReturnMappedObject()
        {
            // Arrange
            var foreignExchangeRateViewModel = new ForeignExchangeRateViewModel
            {
                FromCurrency = "EUR",
                ToCurrency = "USD",
                Bid = 0.5M,
                Ask = 0.2M,
            };

            // Act
            var result = foreignExchangeRateViewModel.ToDomain();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(foreignExchangeRateViewModel.FromCurrency, result.FromCurrency);
            Assert.Equal(foreignExchangeRateViewModel.ToCurrency, result.ToCurrency);
            Assert.Equal(foreignExchangeRateViewModel.Bid, result.Bid);
            Assert.Equal(foreignExchangeRateViewModel.Ask, result.Ask);
        }

        [Fact]
        public void ToViewModel_ValidViewModel_ReturnMappedObject()
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
            var result = foreignExchangeRate.ToViewModel();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(foreignExchangeRate.FromCurrency, result.FromCurrency);
            Assert.Equal(foreignExchangeRate.ToCurrency, result.ToCurrency);
            Assert.Equal(foreignExchangeRate.Bid, result.Bid);
            Assert.Equal(foreignExchangeRate.Ask, result.Ask);
        }
    }
}
