using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using WebApiOData.Entities;

namespace WebApiOData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Odata
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("People");
            builder.EntitySet<Email>("Emails");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
