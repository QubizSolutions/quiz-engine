using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}