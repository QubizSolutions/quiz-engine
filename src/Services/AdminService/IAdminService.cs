using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public interface IAdminService
    {
        Task<Validator[]> AddAdminAsync(Admin admin);
        Task<bool> DeleteAdminAsync(Guid id);
        Task<Validator[]> UpdateAdminAsync(Admin admin);
        Task<Admin[]> GetAllAdminsAsync();
        Task<Admin> GetAdminAsync(Guid id);
    }
}
