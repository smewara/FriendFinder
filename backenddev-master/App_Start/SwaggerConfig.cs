using System.Web.Http;
using Swashbuckle.Application;
using System.Net.Http;
using System;
using System.Web;

namespace FriendFinder
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v2", "FriendFinder");
                    c.RootUrl(GetBasePath);
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi();
        }

        private static string GetBasePath(HttpRequestMessage req)
        {
            return req.RequestUri.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/").TrimEnd('/');
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\FriendFinder.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}