namespace Data.Repository.Mongo
{
    using Application.Domain.Domain;
    using CrossInfrastructure.Exceptions;
    using CrossInfrastructure.Mongo;
    using Data.Entities;
    using Data.Mappers;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<ForeignExchangeRateEntity> _ForeignExchangeRateCollection;

        public MongoRepository(
            IOptions<MongoDBSettings> mongoStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mongoStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoStoreDatabaseSettings.Value.DatabaseName);

            _ForeignExchangeRateCollection = mongoDatabase.GetCollection<ForeignExchangeRateEntity>(
                mongoStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<ForeignExchangeRate?> GetAsync(string fromCurrency, string toCurrency)
        {
            ForeignExchangeRateEntity result = await _ForeignExchangeRateCollection.Find(x => x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency).FirstOrDefaultAsync();

            return result?.ToDomain();
        }

        public async Task CreateAsync(ForeignExchangeRate foreignExchangeRate) =>
            await _ForeignExchangeRateCollection.InsertOneAsync(foreignExchangeRate.ToEntity());

        public async Task UpdateAsync(string fromCurrency, string toCurrency, ForeignExchangeRate foreignExchangeRate) =>
            await _ForeignExchangeRateCollection.ReplaceOneAsync(x => x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency, foreignExchangeRate.ToEntity());

        public async Task RemoveAsync(string fromCurrency, string toCurrency) =>
            await _ForeignExchangeRateCollection.DeleteOneAsync(x => x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency);
    }
}
