using Application.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInfrastructure.Gateways
{
    public interface IExchangeRateGateway
    {
        Task<ForeignExchangeRate> GetForeignExchangeRate(string fromCurrency, string toCurrency);
    }
}
