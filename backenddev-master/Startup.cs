using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(FriendFinder.Startup))]

namespace FriendFinder
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // always use attribute based routing
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // enable CORS (Cross Origin Resource Sharing) 
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            SwaggerConfig.Register(config);
            UnityConfig.RegisterComponents(config);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // enable the web api
            app.UseWebApi(config);

            
        }
    }
}