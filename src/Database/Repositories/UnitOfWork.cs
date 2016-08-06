using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly QuizEngineDataContext dbContext;

		private ISectionRepository sectionRepository;
		private IQuestionRepository questionRepository;
		private IOptionRepository optionRepository;

		public UnitOfWork(IConfig config)
        {
            dbContext = new QuizEngineDataContext(config.ConnectionString);
        }

        public IQuestionRepository QuestionRepository
        {
            get
            {
                if(this.questionRepository == null)
                {
                    this.questionRepository = new QuestionRepository(this.dbContext, this);
                }

                return this.questionRepository;
            }
        }

		public ISectionRepository SectionRepository
		{
			get
			{
				if (this.sectionRepository == null)
				{
					this.sectionRepository = new SectionRepository(this.dbContext, this);
				}

				return this.sectionRepository;
			}
		}

		public IOptionRepository OptionRepository
		{
			get
			{
				if (this.optionRepository == null)
				{
					this.optionRepository = new OptionRepository(this.dbContext, this);
				}

				return this.optionRepository;
			}
		}

		public async Task SaveAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
