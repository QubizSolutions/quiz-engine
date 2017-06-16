﻿using AutoMapper.Runtime.Extensions;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.SectionService.Contract;
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
				Database.Repositories.Section.Contract.Section section = await unitOfWork.SectionRepository.GetByIDAsync(id);
				if (section == null)
					return new ValidationError[1] { new ValidationError() { Message = "Deletion failed! There is no Section instance with this ID!" } };

				unitOfWork.SectionRepository.Delete(section);

				await unitOfWork.SaveAsync();

				return new ValidationError[0];
			}
		}

		public async Task<Section[]> GetAllSectionsAsync()
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				Database.Repositories.Section.Contract.Section[] sections = await unitOfWork.SectionRepository.ListAsync();

				return sections.DeepCopyTo<Section[]>();
			}
		}

		public async Task<ValidationError[]> AddSectionAsync(Section section)
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				Database.Repositories.Section.Contract.Section dbSection = await unitOfWork.SectionRepository.GetByNameAsync(section.Name);
				if (dbSection == null)
				{
					unitOfWork.SectionRepository.Upsert(section.DeepCopyTo<Database.Repositories.Section.Contract.Section>());

					await unitOfWork.SaveAsync();

					return new ValidationError[0];
				}
				return new ValidationError[1] { new ValidationError() { Message = "Add failed! There already exists a Section instance with this name!" } };
			}
		}

		public async Task<ValidationError[]> UpdateSectionAsync(Section section)
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				Database.Repositories.Section.Contract.Section dbSection = await unitOfWork.SectionRepository.GetByNameAsync(section.Name);
				if (dbSection != null && dbSection.ID != section.ID)
					return new ValidationError[1] { new ValidationError() { Message = "Update failed! There is already a Section with this name !" } };

				unitOfWork.SectionRepository.Upsert(section.DeepCopyTo<Qubiz.QuizEngine.Database.Repositories.Section.Contract.Section>());

				await unitOfWork.SaveAsync();

				return new ValidationError[0];
			}
		}

		public async Task<Section> GetSectionAsync(Guid id)
		{
			using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
			{
				Database.Repositories.Section.Contract.Section section = await unitOfWork.SectionRepository.GetByIDAsync(id);

				return section.DeepCopyTo<Section>();
			}
		}
	}
}