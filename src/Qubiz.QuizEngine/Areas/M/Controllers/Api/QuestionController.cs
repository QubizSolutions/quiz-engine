using Qubiz.QuizEngine.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.Controllers.M.Api
{
    public class QuestionController : ApiController
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQuestionsPaged(int id)
        {
            return Ok(await questionService.GetQuestionsByPageAsync(id));
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteQuestion(string id)
        {
            await questionService.DeleteQuestionAsync(Guid.Parse(id));
            return Ok();
        }
    }
}