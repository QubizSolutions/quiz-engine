using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public interface ISectionRepository
	{
		Task<Section.Contract.Section[]> ListAsync();

		Task<Section.Contract.Section> GetByNameAsync(string name);

		Task<Section.Contract.Section> GetByIDAsync(Guid id);

		void Create(Section.Contract.Section section);
		void Update(Section.Contract.Section section);
		void Delete(Section.Contract.Section section);
	}
}