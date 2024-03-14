namespace Application.Domain.Domain
{
    public class ForeignExchangeRate
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }
    }
}
