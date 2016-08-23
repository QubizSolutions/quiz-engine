using Qubiz.QuizEngine.Database.Repositories;

namespace Qubiz.QuizEngine.Services
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
