using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    [Admin]
    public class TemplateController : Controller
    {
        [AllowAnonymous]
        public ActionResult Test()
        {
            return PartialView();
        }

        public ActionResult Exams()
        {
            return PartialView();
        }

        public ActionResult EditAdmin()
        {
            return PartialView();
        }

        public ActionResult Questions()
        {
            return PartialView();
        }

        public ActionResult Sections()
        {
            return PartialView();
        }

        public ActionResult AddAdmin()
        {
            return PartialView();
        }

        public ActionResult Administrators()
        {
            return PartialView();
        }
    }
}