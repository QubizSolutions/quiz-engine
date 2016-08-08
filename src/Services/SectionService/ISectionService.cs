using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;

namespace Qubiz.QuizEngine.Services.SectionService
{
	public interface ISectionService
	{
		Task<Section[]> GetAllSectionsAsync();
		Task<Validator[]> DeleteSectionAsync(Guid id);

	}
}
