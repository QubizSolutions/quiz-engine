using Qubiz.QuizEngine.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class QuestionsController : ApiController
    {
        private readonly IQuestionService questionService;

        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQuestionsPaged(int pageNumber, int itemsPerPage)
        {
            return Ok(await questionService.GetQuestionsByPageAsync(pageNumber, itemsPerPage));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQuestion(String ID)
        {
            return Ok(await questionService.GetQuestionByID(Guid.Parse(ID)));
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutQuestion(Services.Models.QuestionDetail question)
        {
            await questionService.UpdateQuestionAsync(question);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostQuestion(Services.Models.QuestionDetail question)
        {
            await questionService.AddQuestionAsync(question);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteQuestion(string id)
        {
            await questionService.DeleteQuestionAsync(Guid.Parse(id));
            return Ok();
        }
    }
}