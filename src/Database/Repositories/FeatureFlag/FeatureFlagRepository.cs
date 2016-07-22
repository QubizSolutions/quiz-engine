using System;
using System.Linq;
using Qubiz.QuizEngine.Database.Entities;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class FeatureFlagRepository : BaseRepository<FeatureFlag>, IFeatureFlagRepository
    {
        public FeatureFlagRepository(QuizEngineDataContext context, UnitOfWork unitOfWork) 
            : base(context, unitOfWork)
        { }

        public async Task<FeatureFlag[]> GetAllFeatureFlags()
        {
            return await this.dbSet.ToArrayAsync();
        }

        public async Task<FeatureFlag> GetFeatureFlagByID(Guid id)
        {
            return await this.dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
