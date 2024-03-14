namespace CrossInfrastructure.Mongo
{
    using Application.Domain.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMongoRepository
    {
        Task<ForeignExchangeRate?> GetAsync(string fromCurrency, string toCurrency);

        Task CreateAsync(ForeignExchangeRate foreignExchangeRate);

        Task UpdateAsync(string fromCurrency, string toCurrency, ForeignExchangeRate foreignExchangeRate);

        Task RemoveAsync(string fromCurrency, string toCurrency);
    }
}
