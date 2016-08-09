using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.AdminService
{
    public interface IAdminService
    {
        Task<ValidationError[]> AddAdminAsync(Admin admin, string originator);
        Task<ValidationError[]> DeleteAdminAsync(Guid id, string originator);
        Task<ValidationError[]> UpdateAdminAsync(Admin admin, string originator);
        Task<Admin[]> GetAllAdminsAsync();
        Task<Admin> GetAdminAsync(Guid id);
    }
}