using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Qubiz.QuizEngine.Filters;
using Qubiz.QuizEngine.Infrastructure.Logger;


namespace Qubiz.QuizEngine
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}");

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Filters.Add(new WebApiExceptionFilter(DependencyResolver.Current.GetService<ILogger>()));            
        }
    }
}