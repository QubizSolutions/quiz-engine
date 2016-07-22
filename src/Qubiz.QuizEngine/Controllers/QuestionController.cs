using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Linq;
using System.Web.Http;

namespace Qubiz.QuizEngine.Controllers
{
    public class QuestionController : ApiController
    {
		private readonly IQuestionRepository questionRepository;

        public QuestionController(IQuestionRepository repository)
        {
            this.questionRepository = repository;
        }

        public IHttpActionResult Get(Guid id)
        {          
                return Ok(questionRepository.GetQuestionByID(id));
        }

        [HttpGet]
        [Route("api/QuestionsFiltered")]
        public IHttpActionResult GetQuestionsFiltered(Guid? sectionId = null, QuestionType? type = null, int? complexity = null, string filter = "", int pagenumber=0)
        {
            return Ok(questionRepository.GetQuestionsFiltered(sectionId, complexity, type, filter, pagenumber));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/ExamQuestion")]
        public IHttpActionResult GetExamQuestion(Guid id)
        {
            ViewModels.QuestionDetail question = questionRepository.GetQuestionByID(id);
            return Ok(new
            {
                ID = question.ID,
                QuestionText = question.QuestionText,
                Type = question.Type,
                Options = question.Options.Select(o => new { ID = o.ID, Answer = o.Answer}).ToArray()
            });
        }

        public IHttpActionResult Post(ViewModels.QuestionDetail question)
        {
                return Ok(questionRepository.UpdateQuestion(question));
        }

        public IHttpActionResult Delete(Guid id)
        {
                questionRepository.DeleteQuestion(id);
                return Ok();
        }
    }
}
