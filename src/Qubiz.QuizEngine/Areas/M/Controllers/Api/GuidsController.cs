using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class GuidsController : ApiController
    {
        [HttpGet]
        public async Task<Guid[]> Get()
        {
            List<Guid> guids = new List<Guid>();
            for (int i = 1; i <= 10; i++)
            {
                guids.Add(Guid.NewGuid());
            }

            return guids.ToArray();
        }
    }
}
