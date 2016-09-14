using Qubiz.QuizEngine.Database.Repositories.Section.Contract;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public class SectionRepository : ISectionRepository
	{
		private readonly QuizEngineDataContext context;
		private readonly DbSet<Entities.Section> dbSet;

		public SectionRepository(QuizEngineDataContext context)
		{
			this.context = context;
			this.dbSet = context.Set<Entities.Section>();
		}

		public async Task<Section.Contract.Section[]> ListAsync()
		{
			Entities.Section[] sections = await dbSet.ToArrayAsync();

			return sections.DeepCopyTo<Section.Contract.Section[]>();
		}

		public async Task<Section.Contract.Section> GetByNameAsync(string name)
		{
			Entities.Section section = await dbSet.FirstOrDefaultAsync(s => s.Name == name);

			return section.DeepCopyTo<Section.Contract.Section>(); ;
		}

		public async Task<Section.Contract.Section> GetByIDAsync(Guid id)
		{
			Entities.Section section = await dbSet.FirstOrDefaultAsync(s => s.ID == id);

			return section.DeepCopyTo<Section.Contract.Section>(); ;
		}

		public void Delete(Section.Contract.Section section)
		{
			Entities.Section dbSection = dbSet.Find(section.ID);

			if (dbSection != null)
				dbSet.Remove(dbSection);
		}

		public void Upsert(Section.Contract.Section section)
		{
			Entities.Section existingSection = dbSet.Find(section.ID);

			if (existingSection == null)
			{
				dbSet.Add(section.DeepCopyTo<Entities.Section>());
			}
			else
			{
				Mapper.Map(section, existingSection);
			}
		}
	}
}