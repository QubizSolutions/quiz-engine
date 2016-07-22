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

        public async void DeleteQuestion(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<OptionDefinition>> GetAllQuestionDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<OptionDefinition[]> GetOptionsByQuestionIDs(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionDefinition> GetQuestionDefinitionByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid[]> GetQuestionIDsBySctions(Guid[] sectionIDs)
        {
            throw new NotImplementedException();
        }

        public async void UpdateQuestion(QuestionDefinition question)
        {
            throw new NotImplementedException();
        }
    }
}