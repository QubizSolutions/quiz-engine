using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IQuestionRepository
	{
        void UpdateQuestion(QuestionDefinition question);
        void DeleteQuestion(Guid id);
		
        Task<QuestionDefinition> GetQuestionDefinitionByID(Guid id);
        Task<IQueryable<OptionDefinition>> GetAllQuestionDefinitions();
        Task<Guid[]> GetQuestionIDsBySctions(Guid[] sectionIDs);
		Task<OptionDefinition[]> GetOptionsByQuestionIDs(Guid[] ids);
	}
}