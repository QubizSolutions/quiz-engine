using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IOptionRepository
    {
        void UpdateOptions(Guid questionID, OptionDefinition[] options);
        void DeleteOptions(OptionDefinition[] options);
        Task<OptionDefinition[]> GetOptionsByQuestionIDs(Guid[] ids);
	}
}