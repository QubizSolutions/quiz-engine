using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin[]> ListAsync();
        Task<Admin> GetByIDAsync(Guid id);
        Task<Admin> GetByNameAsync(string name);
    }
}