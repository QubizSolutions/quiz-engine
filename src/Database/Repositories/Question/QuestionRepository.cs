using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionDefinition>, IQuestionRepository
    {
        public QuestionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async Task<IQueryable<QuestionDefinition>> GetQuestionsAsync()
        {
            return dbSet;
        }

        public async Task UpdateQuestionAsync(QuestionDefinition question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            QuestionDefinition question = dbSet.Where(i => i.ID == id).ToList()[0];
            dbSet.Remove(question);
        }
    }
}