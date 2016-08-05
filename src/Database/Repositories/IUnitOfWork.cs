using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }
		IOptionRepository OptionRepository { get; }
		Task SaveAsync();
    }
}
