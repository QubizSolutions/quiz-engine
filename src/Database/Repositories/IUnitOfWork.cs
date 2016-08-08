using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IUnitOfWork : IDisposable
	{
        IFeatureFlagRepository FeatureFlagRepository { get; }
		ISectionRepository SectionRepository { get; }

		Task SaveAsync();
    }
}
