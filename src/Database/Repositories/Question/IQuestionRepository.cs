using Qubiz.QuizEngine.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IQuestionRepository
	{
        Task UpdateQuestionAsync(QuestionDefinition question);

        Task DeleteQuestionAsync(Guid id);

		Task<IEnumerable<QuestionDefinition>> GetQuestionsAsync();

        Task<QuestionDefinition> GetQuestionByIDAsync(Guid id);
	}
}