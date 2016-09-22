using Qubiz.QuizEngine.Database.Repositories.Section.Contract;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IAdminRepository AdminRepository { get; }
		IFeatureFlagRepository FeatureFlagRepository { get; }
		IQuestionRepository QuestionRepository { get; }
		IOptionRepository OptionRepository { get; }
		ISectionRepository SectionRepository { get; }
		Task SaveAsync();
	}
}
