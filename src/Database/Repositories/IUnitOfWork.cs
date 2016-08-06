using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFeatureFlagRepository FeatureFlagRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IOptionRepository OptionRepository { get; }
        ISectionRepository SectionRepository { get; }
        Task SaveAsync();
    }
}