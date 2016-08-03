using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class TemplateController : Controller
    {
        // GET: M/TemplateName
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

        public ActionResult Sections()
        {
            return PartialView();
        }

        public ActionResult Administrators()
        {
            return PartialView();
        }
    }
}