namespace Data.Entities
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class ForeignExchangeRateEntity
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }
    }
}
