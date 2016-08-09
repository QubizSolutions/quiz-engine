using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
     public interface ISectionRepository : IBaseRepository<Section>
	{
        Task<Section[]> GetAllSectionsAsync();

		Task<Section> GetSectionByNameAsync(string name);

		Task<Section> GetSectionByIDAsync(Guid id);

	}
}