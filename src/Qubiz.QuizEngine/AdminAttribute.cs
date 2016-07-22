using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine
{
    public class AdminAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext) && Authorizer.IsAdmin(httpContext.User.Identity.Name);
        }
    }
}