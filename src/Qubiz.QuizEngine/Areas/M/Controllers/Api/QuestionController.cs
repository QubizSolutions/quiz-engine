using Qubiz.QuizEngine.Areas.M.Models;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
	public class QuestionController : ApiController
	{
		private readonly IQuestionService questionService;

		public QuestionController(IQuestionService questionService)
		{
			this.questionService = questionService;
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get(int pageNumber, int itemsPerPage)
		{
			Qubiz.QuizEngine.Services.Models.PagedResult<Qubiz.QuizEngine.Services.Models.QuestionListItem> question = await questionService.GetQuestionsByPageAsync(pageNumber, itemsPerPage);
			return Ok(question.DeepCopyTo<QuestionPaged>());
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get(Guid id)
		{
			Qubiz.QuizEngine.Services.Models.QuestionDetail question = await questionService.GetQuestionByID(id);
			return Ok(question.DeepCopyTo<Question>());
		}

		[HttpPut]
		public async Task<IHttpActionResult> Put(Question question)
		{
			await questionService.UpdateQuestionAsync(question.DeepCopyTo<Qubiz.QuizEngine.Services.Models.QuestionDetail>());
			return Ok();
		}

		[HttpPost]
		public async Task<IHttpActionResult> Post(Question question)
		{
			await questionService.AddQuestionAsync(question.DeepCopyTo<Qubiz.QuizEngine.Services.Models.QuestionDetail>());
			return Ok();
		}

		[HttpDelete]
		public async Task<IHttpActionResult> Delete(Guid id)
		{
			await questionService.DeleteQuestionAsync(id);
			return Ok();
		}
	}
}