using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly QuizEngineDataContext dbContext;

        private IFeatureFlagRepository featureFlagRepository;
        private IAdminRepository adminsRepository;

        public UnitOfWork(IConfig config)
        {
            dbContext = new QuizEngineDataContext(config.ConnectionString);
        }

        public IFeatureFlagRepository FeatureFlagRepository
        {
            get
            {
                if (featureFlagRepository == null)
                {
                    featureFlagRepository = new FeatureFlagRepository(dbContext, this);
                }

                return featureFlagRepository;
            }
        }

        public IAdminRepository AdminRepository
        {
            get
            {
                if (adminsRepository == null)
                {
                    adminsRepository = new AdminRepository(dbContext, this);
                }

                return adminsRepository;
            }
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}