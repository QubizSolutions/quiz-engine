using Qubiz.QuizEngine.Infrastructure.Logger;
using System.Web.Mvc;

namespace Qubiz.QuizEngine.Filters
{
    public class MvcExceptionFilter : HandleErrorAttribute
    {
        private readonly ILogger logger;

        public MvcExceptionFilter(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            logger.LogException(filterContext.Exception);
        }
    }
}