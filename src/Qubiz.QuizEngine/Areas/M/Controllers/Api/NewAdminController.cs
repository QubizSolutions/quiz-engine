using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Services.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
    public class NewAdminController : ApiController
    {
        public static List<Admin> admins = new List<Admin> { new Admin() { Name = "Jim Carrey" }, new Admin { Name = "Bruce Lee" } };

        private readonly IAdminService adminService;

        public NewAdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        [Route("M/api/admin/getAdmins")]
        public IHttpActionResult GetAdmins()
        {
            Admin[] admin = adminService.GetAllAdmins().Result;
            return Ok(admin);
        }

        [HttpGet]
        public IHttpActionResult GetLoggedIn()
        {
            string Logged = HttpContext.Current.User.Identity.Name;
            return Ok(Logged);
        }

        [HttpPost]
        public void AddAdmin([FromBody]Admin admin)
        {
            adminService.AddAdmin(admin);
        }

        [HttpDelete]
        public IHttpActionResult DeleteAdmin(Guid id)
        {
            if (adminService.DeleteAdmin(id))
                return Ok();

            return NotFound();
        }

        [HttpPut]
        public void UpdateAdmin([FromBody]Admin admin)
        {
            adminService.UpdateAdmin(admin);
        }
    }
}