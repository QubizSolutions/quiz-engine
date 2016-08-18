using Qubiz.QuizEngine.Database.Repositories;

namespace Qubiz.QuizEngine.Database
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
