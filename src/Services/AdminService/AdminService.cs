using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public class AdminService : IAdminService
    {  
        private readonly IUnitOfWork unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Validator[]> AddAdminAsync(Admin admin)
        {
            List<Admin> admins = new List<Admin>(await unitOfWork.AdminRepository.GetAllAdminsAsync());
            Validator[] validator = new Validator[0];
            Admin if_admin_exists = admins.Find(a => a.Name.Equals(admin.Name));
            if (!admins.Contains(if_admin_exists))
                {
                admin.ID = Guid.NewGuid();
                unitOfWork.AdminRepository.Create(admin);
                await unitOfWork.SaveAsync();
                }
            else
            {
                validator[validator.Length - 1] = new Validator("name already exists");
            }
            return validator;  
        }

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
            Admin admin = await unitOfWork.AdminRepository.GetByIDAsync(id);
            if (admin.Name == HttpContext.Current.User.Identity.Name)
            {
                return false;
            }
            else
            {
                unitOfWork.AdminRepository.Delete(admin);
                await unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task<Admin> GetAdminAsync(Guid id)
        {
            return await unitOfWork.AdminRepository.GetByIDAsync(id);
        }

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            return await unitOfWork.AdminRepository.GetAllAdminsAsync();
            
        }

        public async Task<Validator[]> UpdateAdminAsync(Admin admin)
        {
            Validator[] validator = new Validator[0];
            Admin originalAdmin = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);
            if (admin.Name == HttpContext.Current.User.Identity.Name)
            {
                validator[validator.Length - 1] = new Validator("You cannot change yourself");
            }
            else
            {
                originalAdmin.Name = admin.Name;
                unitOfWork.AdminRepository.Update(originalAdmin);
                await unitOfWork.SaveAsync();
            }
            return validator;
            
        }
    }
}
