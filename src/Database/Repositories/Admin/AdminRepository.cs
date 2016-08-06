using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            return await dbSet.ToArrayAsync();
        }

        public async Task<Admin> GetByIDAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Admin> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}