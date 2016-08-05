using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface ISectionRepository
	{
        void UpdateSections(Section[] sections);
        Task<IQueryable<Section>> GetAllSections();

    }
}