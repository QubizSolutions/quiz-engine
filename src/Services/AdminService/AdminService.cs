using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;

namespace Qubiz.QuizEngine.Services.AdminService
{
    class AdminService : IAdminService
    {

        /*
         * 
         * IAdmin repository will be replaced with unit of work
         * 
         */
         

        private readonly IUnitOfWork UnitOfWork;

        public AdminService(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public async void AddAdminAsync(Admin admin)
        {
            //await UnitOfWork.AdminRepository.AddAdmin(admin);

                throw new NotImplementedException();
        }

        public bool DeleteAdminAsync(Guid id)
        {
            //await UnitOfWork.AdminRepository.DeleteAdmin(id);
            return false;
        }

        public Task<Admin> GetAdminAsync(Guid id)
        {
            //return UnitOfWork.AdminRepository.GetAllAdmins(id);

            throw new NotImplementedException();
        }

        public Task<Admin[]> GetAllAdminsAsync()
        {
            return UnitOfWork.AdminRepository.GetAllAdminsAsync();
            
        }

        public async void UpdateAdminAsync(Admin admin)
        {
            //await UnitOfWork.AdminRepository.UpdateAdmin(admin);
            throw new NotImplementedException();
        }
    }
}
