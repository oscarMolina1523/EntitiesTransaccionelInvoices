//using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Http;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.UseDataContractJsonSerializer = true;
            //json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            //var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //formatter.SerializerSettings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    TypeNameHandling = TypeNameHandling.Objects,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
