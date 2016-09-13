using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public class SectionRepository : ISectionRepository
	{
		private DbContext dbContext;
		private DbSet<Entities.Section> dbSet;
		private UnitOfWork unitOfWork;

		public SectionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
		{
			this.dbContext = context;
			this.dbSet = dbContext.Set<Entities.Section>();
			this.unitOfWork = unitOfWork;
		}

		public async Task<Section.Contract.Section[]> ListAsync()
		{
			Entities.Section[] dbSections = await dbSet.ToArrayAsync();
			Section.Contract.Section[] sections = dbSections.DeepCopyTo<Section.Contract.Section[]>();
			return sections;
		}

		public async Task<Section.Contract.Section> GetByNameAsync(string name)
		{
			Entities.Section dbSection = await dbSet.FirstOrDefaultAsync(s => s.Name == name);
			Section.Contract.Section section = dbSection.DeepCopyTo<Section.Contract.Section>();
			return section;
		}

		public async Task<Section.Contract.Section> GetByIDAsync(Guid id)
		{
			Entities.Section dbSection = await dbSet.FirstOrDefaultAsync(s => s.ID == id);
			Section.Contract.Section section = dbSection.DeepCopyTo<Section.Contract.Section>();
			return section;
		}

		public void Create(Section.Contract.Section section)
		{
			dbSet.Add(section.DeepCopyTo<Entities.Section>());

			dbContext.SaveChanges();
		}

		public void Update(Section.Contract.Section section)
		{
			var dbSection = dbSet.Find(section.ID);
			if (dbSection != null)
			{
				dbSection.Name = section.Name;
				dbSet.Attach(dbSection);
			}
			dbContext.Entry(dbSection).State = EntityState.Modified;

			dbContext.SaveChanges();
		}

		public void Delete(Section.Contract.Section section)
		{
			var dbSection = dbSet.Find(section.ID);

			if (dbContext.Entry(dbSection).State == EntityState.Detached)
			{
				dbSet.Attach(dbSection);
			}

			dbSet.Remove(dbSection);

			dbContext.SaveChanges();
		}
		public virtual void Upsert(Section.Contract.Section section)
		{
			if (section == null) throw new System.NullReferenceException("Value cannot be null");

			Entities.Section existingSection = dbContext.Set<Entities.Section>().Find(section.DeepCopyTo<Entities.Section>().ID);

			if (existingSection == null)
			{
				dbContext.Set<Entities.Section>().Add(section.DeepCopyTo<Entities.Section>());
			}
			else
			{
				Mapper.Map(section, existingSection);
			}
			dbContext.SaveChanges();
		}
	}
}