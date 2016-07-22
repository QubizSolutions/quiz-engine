using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class OptionnRepository : BaseRepository<OptionDefinition>, IOptionRepository
    {
        public OptionnRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public void DeleteOptions(OptionDefinition[] options)
        {
            throw new NotImplementedException();
        }

        public Task<OptionDefinition[]> GetOptionsByQuestionIDs(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void UpdateOptions(Guid questionID, OptionDefinition[] options)
        {
            throw new NotImplementedException();
        }
    }
}