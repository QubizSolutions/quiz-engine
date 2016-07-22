using Qubiz.QuizEngine.Core;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Qubiz.QuizEngine.Controllers
{
    public class TestController : ApiController
    {
		private readonly ITestRepository testRepository;
		private readonly IQuestionRepository questionRepository;

        public TestController(ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
        }
        [AllowAnonymous]
        public IHttpActionResult Get(Guid id)
        {
            ViewModels.TestDefinitionDetail test = testRepository.GetTestByID(id);

            if (test.IsPublished || Authorizer.IsAdmin(HttpContext.Current.User.Identity.Name))
                return Ok(test);
            else
                return StatusCode(System.Net.HttpStatusCode.Unauthorized);
        }

        [AllowAnonymous]
        public IHttpActionResult Get(string filter = "")
        {
                return Ok(testRepository.GetTestsFiltered(filter, Authorizer.IsAdmin(HttpContext.Current.User.Identity.Name)));
        }
        public IHttpActionResult Post(ViewModels.TestDefinitionDetail test)
        {
                testRepository.UpdateTest(test);
                return Ok();
        }
        public IHttpActionResult Delete(Guid id)
        {
                testRepository.DeleteTest(id);
                return Ok();
        }
    }
}
