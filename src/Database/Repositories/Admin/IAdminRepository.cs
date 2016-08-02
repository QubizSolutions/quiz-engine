using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin[]> GetAllAdmins();
        Task<Admin> GetByID(Guid id);
        Task<Admin> GetByName(string Name);
    }
}