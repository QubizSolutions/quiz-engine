using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Qubiz.QuizEngine.Infrastructure.Logger;

namespace Qubiz.QuizEngine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Initialise();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);         
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_Error(object sender, EventArgs e)
        {          
            try
            {
                System.Exception error = Server.GetLastError();
                if ((error.Message.IndexOf("Server cannot set status after HTTP headers have been sent") >= 0) || (error.Message.IndexOf("The controller for path ") >= 0))
                    LogHelper.LogInfo(error.Message);
                else
                    LogHelper.LogError(error);
            }
            catch
            { }             
        }

    }
}
