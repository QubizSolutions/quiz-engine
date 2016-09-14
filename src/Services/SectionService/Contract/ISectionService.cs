using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.SectionService.Contract
{
	public interface ISectionService
	{
		Task<Section[]> GetAllSectionsAsync();

		Task<ValidationError[]> DeleteSectionAsync(Guid id);

		Task<ValidationError[]> AddSectionAsync(Section section);

		Task<ValidationError[]> UpdateSectionAsync(Section section);

		Task<Section> GetSectionAsync(Guid id);
	}
}