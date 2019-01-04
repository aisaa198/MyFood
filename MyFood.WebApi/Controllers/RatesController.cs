using MyFood.BL.Models;
using MyFood.BL.Services;
using System;
using System.Web.Http;

namespace MyFood.WebApi.Controllers
{
    public class RatesController : ApiController
    {
        private readonly IRatesService _ratesService;

        public RatesController(IRatesService ratesService)
        {
            _ratesService = ratesService;
        }

        [HttpPost]
        [Route("api/rates/add")]
        public Guid AddRate([FromBody] RateDto rate)
        {
            return _ratesService.AddRate(rate);
        }
    }
}
