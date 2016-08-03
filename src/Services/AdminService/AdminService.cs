using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using System;
using System.Threading.Tasks;

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
            //await unitOfWork.AdminRepository.AddAdmin(admin);

            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
         //   await UnitOfWork.AdminRepository.DeleteAdmin(id);
            return false;
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
            //await UnitOfWork.AdminRepository.UpdateAdmin(admin);
            throw new NotImplementedException();
        }
    }
}
