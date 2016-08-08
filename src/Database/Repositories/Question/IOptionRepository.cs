using Qubiz.QuizEngine.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IOptionRepository
    {
        void UpdateOptionsAsync(Guid questionID, OptionDefinition[] options);
        void DeleteOptionsAsync(OptionDefinition[] options);
        Task<IEnumerable<OptionDefinition>> GetAllOptionsAsync();
	}
}