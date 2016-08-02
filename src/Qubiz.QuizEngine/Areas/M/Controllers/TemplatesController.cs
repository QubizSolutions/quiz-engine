using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class TemplatesController : Controller
    {

        public ActionResult Index()
        {
            return PartialView();
        }
       
        public ActionResult Test()
        {
            return PartialView();
        }
    }
}