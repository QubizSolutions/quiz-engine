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
		void DeleteQuestion(Guid id);
		Task<IQueryable<QuestionDefinition>> GetQuestions();
		
	}
}