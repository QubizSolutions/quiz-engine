using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizEngineDataContext dbContext;
        
        private IFeatureFlagRepository featureFlagRepository;
		private ISectionRepository sectionRepository;

        public UnitOfWork(IConfig config)
        {
            dbContext = new QuizEngineDataContext(config.ConnectionString);
        }

        public IFeatureFlagRepository FeatureFlagRepository
        {
            get
            {
                if(featureFlagRepository == null)
                {
                    featureFlagRepository = new FeatureFlagRepository(this.dbContext, this);
                }

                return featureFlagRepository;
            }
        }
        
		public ISectionRepository SectionRepository
		{
			get
			{
				if(sectionRepository == null)
				{
					sectionRepository = new SectionRepository(this.dbContext, this);
				}
				return sectionRepository;
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
