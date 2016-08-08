using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async Task<IQueryable<Section>> GetAllSections()
        {
			return dbSet;
        }

        public async void UpdateSections(Section[] sections)
        {
            throw new NotImplementedException();
        }
    }
}