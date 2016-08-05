using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Services;
using System.Threading.Tasks;

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
		public async Task<IHttpActionResult> GetQuestionsPaged(int id)
		{
            return Ok(await questionService.GetQuestionsByPage(id));
		}

        [HttpDelete]
        public IHttpActionResult DeleteQuestion(string id)
        {
            questionService.DeleteQuestion(Guid.Parse(id));
            return Ok();
        }

	}
}
