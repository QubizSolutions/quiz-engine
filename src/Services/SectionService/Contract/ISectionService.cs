using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.SectionService
{
	public interface ISectionService
	{
		Task<Contract.Section[]> GetAllSectionsAsync();

		Task<ValidationError[]> DeleteSectionAsync(Guid id);

		Task<ValidationError[]> AddSectionAsync(Contract.Section section);

		Task<ValidationError[]> UpdateSectionAsync(Contract.Section section);

		Task<Contract.Section> GetSectionAsync(Guid id);
	}
}