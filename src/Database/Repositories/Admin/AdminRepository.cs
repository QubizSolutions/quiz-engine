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

        public async Task<Admin[]> GetAllAdminsAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }
        
        public async Task<Admin> GetByIDAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Admin> GetByNameAsync(string Name)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Name == Name);
        }
    }
}