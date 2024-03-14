namespace ForeignExchangeRates.Controllers
{
    using CrossInfrastructure.Services;
    using ForeignExchangeRates.Api.Mappers;
    using ForeignExchangeRates.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.RegularExpressions;

    [ApiController]
    [Route("api/[controller]")]
    public class ForeignExchangeRatesController : ControllerBase
    {
        private readonly ILogger<ForeignExchangeRatesController> _logger;
        private IForeignExchangeRatesService foreignExchangeRatesService;

        public ForeignExchangeRatesController(
            IForeignExchangeRatesService foreignExchangeRatesService,
            ILogger<ForeignExchangeRatesController> logger)
        {
            this.foreignExchangeRatesService = foreignExchangeRatesService;
            _logger = logger;
        }

        [HttpGet(Name = "GetForeignExchangeRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] string fromCurrency, [FromQuery] string toCurrency)
        {
            if (!this.ValidateInputCurrencies(fromCurrency, toCurrency))
            {
                return BadRequest("Input currencies are invalid. Please validate the requested currencies.  Ex: USD");
            }

            var result = await this.foreignExchangeRatesService.GetForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);

            return Ok(result?.ToViewModel());
        }

        [HttpDelete(Name = "DeleteForeignExchangeRate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string fromCurrency, string toCurrency)
        {
            if (!this.ValidateInputCurrencies(fromCurrency, toCurrency))
            {
                return BadRequest("Input currencies are invalid. Please validate the requested currencies. Ex: USD");
            }

            await this.foreignExchangeRatesService.DeleteForeignExchangeRate(fromCurrency, toCurrency).ConfigureAwait(false);
            
            return NoContent();
        }

        [HttpPatch(Name = "PatchForeignExchangeRate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(ForeignExchangeRateViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await this.foreignExchangeRatesService.UpdateForeignExchangeRate(model.ToDomain()).ConfigureAwait(false);

            return NoContent();
        }

        private bool ValidateInputCurrencies(string from, string to)
        {
            return from != null
                && to != null
                && Regex.Match(from, "[A-Z]{3}").Success
                && Regex.Match(to, "[A-Z]{3}").Success;
        }
    }
}
