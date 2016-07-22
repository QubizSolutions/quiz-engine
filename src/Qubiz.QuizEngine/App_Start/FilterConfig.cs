using Qubiz.QuizEngine.Infrastructure.Logger;
using Qubiz.QuizEngine.Filters;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionFilter(DependencyResolver.Current.GetService<ILogger>()));
            filters.Add(new AdminAttribute());
        }        
    }
}
