using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
     public interface ISectionRepository : IBaseRepository<Section>
	{
        Task<Section[]> ListAsync();

		Task<Section> GetByNameAsync(string name);

		Task<Section> GetByIDAsync(Guid id);

	}
}