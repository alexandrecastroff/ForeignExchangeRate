using System.ComponentModel.DataAnnotations;

namespace ForeignExchangeRates.Api.Models
{
    public class ForeignExchangeRateViewModel
    {
        [Required]
        [RegularExpression("[A-Z]{3}",ErrorMessage = "Invalid currency code.")]
        public string FromCurrency { get; set; }

        [Required]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "Invalid currency code.")]
        public string ToCurrency { get; set; }

        [Required]
        public decimal Bid { get; set; }

        [Required]
        public decimal Ask { get; set; }
}
}
