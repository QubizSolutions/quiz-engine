using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories.Section.Contract
{
	public interface ISectionRepository
	{
		Task<Section[]> ListAsync();

		Task<Section> GetByNameAsync(string name);

		Task<Section> GetByIDAsync(Guid id);

		void Delete(Section section);

		void Upsert(Section section);
	}
}