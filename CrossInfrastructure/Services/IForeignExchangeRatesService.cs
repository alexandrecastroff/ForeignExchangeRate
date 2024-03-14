namespace CrossInfrastructure.Services
{
    using Application.Domain.Domain;
    using System.Threading.Tasks;

    public interface IForeignExchangeRatesService
    {
        Task<ForeignExchangeRate> GetForeignExchangeRate(string fromCurrency, string toCurrency);

        Task DeleteForeignExchangeRate(string fromCurrency, string toCurrency);

        Task UpdateForeignExchangeRate(ForeignExchangeRate foreignExchangeRate);
    }
}
