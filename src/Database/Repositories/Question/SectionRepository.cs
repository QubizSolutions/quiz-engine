using Qubiz.QuizEngine.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public class SectionRepository : BaseRepository<Entities.Section>, ISectionRepository
    {
        public SectionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync()
        {
            return dbSet.Select(s => new Section
            {
                ID = s.ID,
                Name = s.Name
            }).ToList();
        }

        public async void UpdateSectionsAsync(Section[] sections)
        {
            throw new NotImplementedException();
        }
    }
}