using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IQuestionRepository
	{
        void UpdateQuestion(QuestionDefinition question);
		Task<IQueryable<QuestionDefinition>> DeleteQuestion(Guid id);
		Task<Models.PagedResult<Models.QuestionListItem>> GetQuestionsByPage(int pagenumber);
		
	}
}