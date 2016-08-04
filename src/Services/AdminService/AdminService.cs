using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
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

        public async Task AddAdminAsync(Admin admin)
        {
            List<Admin> admins = new List<Admin>(await unitOfWork.AdminRepository.GetAllAdminsAsync());


           
                Admin if_admin_exists = admins.Find(a => a.Name.Equals(admin.Name));
                if (!admins.Contains(if_admin_exists))
                {
                admin.ID = new Guid();
                unitOfWork.AdminRepository.Create(admin);
                await unitOfWork.SaveAsync();
            }
                
            
            
            

            
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
            //return UnitOfWork.AdminRepository.GetAllAdmins(id);

            throw new NotImplementedException();
        }

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            return await unitOfWork.AdminRepository.GetAllAdminsAsync();
            
        }

        public async void UpdateAdminAsync(Admin admin)
        {
            Admin admin2 = await unitOfWork.AdminRepository.GetByIDAsync(admin.ID);
            if (admin.Name == HttpContext.Current.User.Identity.Name)
            {
                return ;
            }
            else
            {
                unitOfWork.AdminRepository.Update(admin);
                await unitOfWork.SaveAsync();
            }
            
        }
    }
}
