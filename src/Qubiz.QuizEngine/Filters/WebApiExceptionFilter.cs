using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using Qubiz.QuizEngine.Infrastructure.Logger;

namespace Qubiz.QuizEngine.Filters
{
    public class WebApiExceptionFilter: ExceptionFilterAttribute
     {
         private readonly ILogger logger;

         public WebApiExceptionFilter(ILogger logger)
         {
             this.logger = logger;
         }

         public override void OnException(HttpActionExecutedContext actionExecutedContext)
         {
             logger.LogException(actionExecutedContext.Exception);

             actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
         }
     }
}