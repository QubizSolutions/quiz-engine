using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class TestController : ApiController
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
