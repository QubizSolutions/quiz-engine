using Qubiz.QuizEngine.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class NewQuestionController : ApiController
    {
        private readonly IQuestionService questionService;

        public NewQuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQuestionsPaged(int pageNumber, int itemsPerPage)
        {
            return Ok(await questionService.GetQuestionsByPageAsync(pageNumber, itemsPerPage));
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteQuestion(string id)
        {
            await questionService.DeleteQuestionAsync(Guid.Parse(id));
            return Ok();
        }
    }
}