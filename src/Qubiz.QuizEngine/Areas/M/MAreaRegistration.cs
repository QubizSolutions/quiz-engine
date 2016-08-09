using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Qubiz.QuizEngine.Areas.M
{
    public class MAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "M";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterRoutes(context);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterRoutes(AreaRegistrationContext context)
        {  
            context.Routes.MapHttpRoute(
                name: "MApiAction",
                routeTemplate: "M/api/{controller}/{action}"
            );

            context.Routes.MapHttpRoute(
                name: "MApiActionId",
                routeTemplate: "M/api/{controller}/{action}/{id}"
            );

            context.Routes.MapHttpRoute(
                name: "MApi",
                routeTemplate: "M/api/{controller}"
            );

            context.MapRoute(
              "M_default",
              "M/{controller}/{action}/{id}",
              new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              new[] { "Qubiz.QuizEngine.Areas.M.Controllers" }
            );
        }
    }
}