namespace Application.Gateways.Implementations
{
    using Newtonsoft.Json;

    public class MetaData
    {
        [JsonProperty("8. Bid Price")]
        public string BidPrice { get; set; }

        [JsonProperty("9. Ask Price")]
        public string AskPrice { get; set; }
    }
}
