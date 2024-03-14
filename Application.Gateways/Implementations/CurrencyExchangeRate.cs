namespace Application.Gateways.Implementations
{
    using Newtonsoft.Json;

    public class CurrencyExchangeRate
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public MetaData MetaData { get; set; }
    }
}
