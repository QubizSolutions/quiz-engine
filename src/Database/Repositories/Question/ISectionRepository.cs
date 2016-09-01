using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
     public interface ISectionRepository : IBaseRepository<Section>
	{
        Task<Section[]> List();

		Task<Section> GetByName(string name);

		Task<Section> GetByID(Guid id);

	}
}