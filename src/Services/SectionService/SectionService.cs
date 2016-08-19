using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.SectionService
{
	public class SectionService : ISectionService
	{
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public SectionService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<ValidationError[]> DeleteSectionAsync(Guid id)
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				Section section = await unitOfWork.SectionRepository.GetSectionByIDAsync(id);
				if (section == null)
					return new ValidationError[1] { new ValidationError() { Message = "There is no Section instance with this ID!" } };

				unitOfWork.SectionRepository.Delete(section);

				await unitOfWork.SaveAsync();

				return new ValidationError[0];
			}
		}

		public async Task<Section[]> GetAllSectionsAsync()
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				return await unitOfWork.SectionRepository.GetAllSectionsAsync();
			}
		}
	}
}