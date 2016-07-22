using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qubiz.QuizEngine.Controllers
{
    public class TemplatesController : Controller
    {
        [OutputCache(Duration=60)]
        public ActionResult Sections()
        {
           return PartialView();
        }

		[OutputCache(Duration = 60)]
		public ActionResult AnswersDetails()
		{
			return PartialView();
		}

        [OutputCache(Duration = 60)]
        public ActionResult Dropdown()
        {
            return PartialView();
        }

		[OutputCache(Duration = 60)]
        public ActionResult Admins()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult SectionSelector()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult QuestionList()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult QuestionEditor()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult QuestionSelector()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [OutputCache(Duration = 60)]
        public ActionResult TestList()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult TestEditor()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [OutputCache(Duration = 60)]
        public ActionResult TakeExam()
        {
            return PartialView();
        }

        [OutputCache(Duration = 60)]
        public ActionResult ExamList() 
        {
            return PartialView();
        }
        [OutputCache(Duration = 60)]
        public ActionResult ExamDetail()
        {
            return PartialView();
        }
    }
}
