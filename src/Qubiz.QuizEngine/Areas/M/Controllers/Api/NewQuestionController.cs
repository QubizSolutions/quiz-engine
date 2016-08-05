using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Services;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
	public class NewQuestionController : ApiController
	{

		private readonly IQuestionService questionService;

		public NewQuestionController(IQuestionService service)
		{
			this.questionService = service;
		}

		[HttpGet]
		public IHttpActionResult GetQuestionsFiltered(int id)
		{
			return Ok(questionService.GetQuestionsByPage(id));
		}

	}
}
