using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.AdminService;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class NewAdminController : ApiController
    {
        private readonly IAdminService adminService;

        public NewAdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAdmin(Guid id)
        {
            Admin admin = await adminService.GetAdminAsync(id);
            return Ok(admin);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAdmins()
        {
            Admin[] admins = await adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddAdmin([FromBody]Admin admin)
        {
            ValidationError[] validationErrors = await adminService.AddAdminAsync(admin, User.Identity.Name);
            if (validationErrors.Length == 0)
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAdmin(Guid id)
        {
            ValidationError[] validationErrors = await adminService.DeleteAdminAsync(id, User.Identity.Name);

            if (validationErrors.Length == 0)
            {
                ApplicationMemoryCache.Instance["GetAllAdmins()"] = null;
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAdmin([FromBody]Admin admin)
        {
            ValidationError[] validationErrors = await adminService.UpdateAdminAsync(admin, User.Identity.Name);

            if (validationErrors.Length == 0)
            {
                ApplicationMemoryCache.Instance["GetAllAdmins()"] = null;
                return Ok();
            }

            return BadRequest();
        }
    }
}