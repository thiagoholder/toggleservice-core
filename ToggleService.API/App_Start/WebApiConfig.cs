using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using Unity.WebApi;

namespace ToggleService.API
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {

            var config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(
                    UnityConfig.RegisterComponents())
            };
            config.MapHttpAttributeRoutes();
         
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Toggle Service Api");
                c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Swagger.XML");
                c.ResolveConflictingActions(x => x.First());

            }).EnableSwaggerUi();

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting
                = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
            
            return config;
        }
        
    }
}
