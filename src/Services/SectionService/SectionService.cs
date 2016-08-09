using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services.SectionService
{
	public class SectionService : ISectionService
	{
		private readonly IConfig config;

		public SectionService(IConfig config)
		{
			this.config = config;
		}

		public async Task<ValidationError[]> DeleteSectionAsync(Guid id)
		{
			using (IUnitOfWork unitOfWork = new UnitOfWork(config))
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
			using (IUnitOfWork unitOfWork = new UnitOfWork(config))
			{
				return await unitOfWork.SectionRepository.GetAllSectionsAsync();
			}
		}
	}
}