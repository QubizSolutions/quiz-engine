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

        public ActionResult Questions()
        {
            return PartialView();
        }
        public ActionResult QuestionsEdit()
        {
            return PartialView();
        }

        public ActionResult QuestionsAdd()
        {
            return PartialView();
        }

        public ActionResult Sections()
        {
            return PartialView();
        }
        public ActionResult SaveAdmin()
        {
            return PartialView();
        }
        public ActionResult Administrators()
        {
            return PartialView();
        }
        public ActionResult AddSection()
        {
            return PartialView();
        }
    }
}