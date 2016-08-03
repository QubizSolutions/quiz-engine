using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public interface IAdminService
    {
        Task AddAdminAsync(Admin admin);
        Task<bool> DeleteAdminAsync(Guid id);
        void UpdateAdminAsync(Admin admin);
        Task<Admin[]> GetAllAdminsAsync();
        Task<Admin> GetAdminAsync(Guid id);
    }
}
