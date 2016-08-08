using Qubiz.QuizEngine.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface ISectionRepository
	{
        void UpdateSectionsAsync(Section[] sections);
        Task<IEnumerable<Section>> GetAllSectionsAsync();
    }
}