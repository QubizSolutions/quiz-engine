using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Services.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
            Admin admin =await adminService.GetAdminAsync(id);
            return Ok(admin);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAdmins()
        {
            Admin[] admins = await adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        [HttpPost]
        public async Task AddAdmin([FromBody]Admin admin)
        {
            await adminService.AddAdminAsync(admin);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAdmin(Guid id)
        {
            if (await adminService.DeleteAdminAsync(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        public async void UpdateAdmin([FromBody]Admin admin)
        {
            adminService.UpdateAdminAsync(admin);
        }
    }
}