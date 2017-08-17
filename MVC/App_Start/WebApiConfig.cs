using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MOKANZ
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //my custom route template - to enable calling web api action methods explicitly
            config.Routes.MapHttpRoute(
               name:  "WithActionApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
             );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
