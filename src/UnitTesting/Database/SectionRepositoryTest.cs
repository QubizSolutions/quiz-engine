using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using System.Linq;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Database.Repositories.Section.Contract;
using Qubiz.QuizEngine.Database.Repositories.Section;

namespace Qubiz.QuizEngine.UnitTesting.Database
{
	[TestClass]
	public class SectionRepositoryTest
	{
		private QuizEngineDataContext dbContext;
		private SectionRepository sectionRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			IConfig config = new Config();
			dbContext = new QuizEngineDataContext(config.ConnectionString);
			sectionRepository = new SectionRepository(dbContext);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			dbContext.Database.ExecuteSqlCommand("DELETE FROM [dbo].[Sections]");
		}

		[TestMethod]
		public async Task ListAsync_WhenSectionsExists_ThenReturnArrayOfSections()
		{
			Section section1 = new Section { ID = Guid.NewGuid(), Name = "Section1" };
			Section section2 = new Section { ID = Guid.NewGuid(), Name = "Section2" };

			sectionRepository.Upsert(section1);
			sectionRepository.Upsert(section2);
			dbContext.SaveChanges();

			Section[] dbSections = await sectionRepository.ListAsync();

			AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
			AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
		}

		[TestMethod]
		public async Task ListAsync_WhenSectionsDoesNotExist_ThenReturnEmptyList()
		{
			Section[] sections = await sectionRepository.ListAsync();

			Assert.AreEqual(0, sections.Length);
		}

		[TestMethod]
		public async Task GetByIDAsync_WhenSectionExists_ThenReturnsSectionByID()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public async Task GetByIDAsync_WhenSectionDoesNotExist_ThenReturnsNull()
		{
			Guid id = Guid.NewGuid();

			Section section = await sectionRepository.GetByIDAsync(id);

			Assert.IsNull(section);
		}

		[TestMethod]
		public async Task GetByNameAsync_WhenSectionExists_ThenReturnsSectionByName()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByNameAsync(section.Name);

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public async Task GetByNameAsync_WhenSectionDoesNotExist_ThenReturnsNull()
		{
			Section section = await sectionRepository.GetByNameAsync("UnexistingSection");

			Assert.IsNull(section);
		}

		[TestMethod]
		public async Task Delete_WhenSectionExists_ThenSectionIsDeleted()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();
			sectionRepository.Delete(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			Assert.IsNull(dbSection);
		}

		[TestMethod]
		public async Task Delete_WhenSectionDoesNotExist_Success()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			sectionRepository.Delete(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			Assert.IsNull(dbSection);
		}

		[TestMethod]
		public async Task Upsert_WhenSectionDoesNotExist_ThenSectionIsAdded()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public async Task Upsert_WhenSectionExists_ThenSectionIsUpdated()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };
			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			section.Name = "This is a new Name";

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			AssertSectionEqual(section, dbSection);
		}

		private void AssertSectionEqual(Section expected, Section actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Name, actual.Name);
		}
	}
}
