using System;
using System.Web.Http;
using Owin;

namespace SampleOwinHost
{
    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional}
                );

            appBuilder.UseWebApi(config);
        }
    }
}