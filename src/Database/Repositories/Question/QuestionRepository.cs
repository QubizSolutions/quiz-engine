using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Qubiz.QuizEngine.Database.Repositories
{
	public class QuestionRepository : BaseRepository<QuestionDefinition>, IQuestionRepository
	{
		
		public QuestionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

		public async Task<IQueryable<QuestionDefinition>> GetQuestions()
		{
            return dbSet;
		}

        public async void UpdateQuestion(QuestionDefinition question)
        {
			 

		}

		public async void DeleteQuestion(Guid id)
		{
            QuestionDefinition question = dbSet.Where(i => i.ID == id).ToList()[0];
			dbSet.Remove(question);
            dbContext.SaveChanges();
		}
	}
}