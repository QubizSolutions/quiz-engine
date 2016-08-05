using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
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
        public async Task<IHttpActionResult> AddAdmin([FromBody]Admin admin)
        {
            Validator[] validator=await adminService.AddAdminAsync(admin);
            if (validator.Length == 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAdmin(Guid id)
        {
            if (await adminService.DeleteAdminAsync(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAdmin([FromBody]Admin admin)
        {
            Validator[] validator= await adminService.UpdateAdminAsync(admin);
            if (validator.Length == 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}