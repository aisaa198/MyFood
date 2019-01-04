using System.Web.Http;

namespace MyFood.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        public string Get()
        {
            return "ok";
        }
    }
}
