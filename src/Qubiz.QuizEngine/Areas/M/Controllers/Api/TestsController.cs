using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.Controllers.M.Api
{
    public class TestsController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetMessage()
        {
            string message = "Hello! .Net Internship";

            return Ok(message);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetMessage(int id)
        {
            string message = "Hello! .Net Internship; ID=" + id;

            return Ok(message);
        }
    }
}
