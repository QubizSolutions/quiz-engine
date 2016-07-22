using Qubiz.QuizEngine.Database.Entities;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin[]> GetAllAdmins();
        void UpdateAdmins(Admin[] admins);
    }
}