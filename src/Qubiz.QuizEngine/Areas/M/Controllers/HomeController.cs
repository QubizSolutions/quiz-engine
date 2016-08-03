using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
	public class HomeController : Controller
	{
		public static string name = "";

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}