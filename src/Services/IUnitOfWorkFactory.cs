using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;

namespace Qubiz.QuizEngine.Database
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
