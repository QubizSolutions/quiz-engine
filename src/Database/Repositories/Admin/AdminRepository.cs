using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
		
        public AdminRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public Task<Admin[]> GetAllAdmins()
        {
            throw new NotImplementedException();
        }

        public async void UpdateAdmins(Admin[] admins)
        {
            throw new NotImplementedException();
        }
    }
}