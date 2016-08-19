using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Qubiz.QuizEngine.Controllers
{

    public class AdminController : ApiController
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository repository)
        {
            this.adminRepository = repository;
        }

        public IHttpActionResult Get()
        {
                return this.Ok(adminRepository.GetAllAdmins().OrderBy(s => s.Name));
        }
        public IHttpActionResult Post(Admin[] admins)
        {
                adminRepository.UpdateAdmins(admins);
                return Ok();
        }

    }
}
