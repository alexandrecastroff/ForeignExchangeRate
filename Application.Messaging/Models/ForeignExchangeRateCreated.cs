using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messaging.Models
{
    public class ForeignExchangeRateCreated
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public string Bid { get; set; }

        public string Ask { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}
