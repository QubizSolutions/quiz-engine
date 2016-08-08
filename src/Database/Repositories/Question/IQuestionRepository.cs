using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IQuestionRepository
	{
        Task UpdateQuestionAsync(QuestionDefinition question);

        Task DeleteQuestionAsync(Guid id);

		Task<IQueryable<QuestionDefinition>> GetQuestionsAsync();
	}
}