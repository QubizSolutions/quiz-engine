using Qubiz.QuizEngine.Core;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Qubiz.QuizEngine.Controllers
{
    public class SectionsController : ApiController
    {
		private readonly IQuestionRepository questionRepository;

        public SectionsController(IQuestionRepository repository)
        {
            this.questionRepository = repository;
        }

        public IHttpActionResult Get()
        {          
            return Ok(questionRepository.GetAllSections().OrderBy(s => s.Name));
        }

        public IHttpActionResult Post(Section[] sections)
        {
                questionRepository.UpdateSections(sections);
                return Ok();
        }

    }
}
