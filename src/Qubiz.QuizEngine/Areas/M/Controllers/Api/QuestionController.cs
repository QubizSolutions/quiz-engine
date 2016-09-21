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
            return Ok(await questionService.GetQuestionsByPageAsync(pageNumber, itemsPerPage));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid ID)
        {
            return Ok(await questionService.GetQuestionByID(ID));
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(QuestionDetail question)
        {
            await questionService.UpdateQuestionAsync(question.DeepCopyTo<Qubiz.QuizEngine.Services.Models.QuestionDetail>());
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(QuestionDetail question)
        {
            await questionService.AddQuestionAsync(question.DeepCopyTo<Qubiz.QuizEngine.Services.Models.QuestionDetail>());
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid ID)
        {
            await questionService.DeleteQuestionAsync(ID);
            return Ok();
        }
    }
}