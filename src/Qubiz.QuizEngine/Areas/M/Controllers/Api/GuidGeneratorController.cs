using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class GuidGeneratorController : ApiController
    {

        [HttpGet]
        public async Task<IHttpActionResult> GetGuids(int number)
        {
            List<Guid> result = new List<Guid>();
            for(int i=1;i<= number; i++)
            {
                result.Add(Guid.NewGuid());
            }
            return Ok(result);
        }

    }
}
