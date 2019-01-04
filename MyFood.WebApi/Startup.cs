using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace MyFood.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*", "X-Custom-Reader"));
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            app.UseNinjectMiddleware(NinjectBootstrap.GetKernel).UseNinjectWebApi(config);
        }
    }
}
