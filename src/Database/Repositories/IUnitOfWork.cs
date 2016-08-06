using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository AdminRepository { get; }
        IFeatureFlagRepository FeatureFlagRepository { get; }
        Task SaveAsync();
    }
}