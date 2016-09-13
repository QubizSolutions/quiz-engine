using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Database.Repositories.Section.Contract;

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
			sectionRepository = new SectionRepository(dbContext, null);
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
			dbContext.SaveChanges();
			sectionRepository.Upsert(section2);
			dbContext.SaveChanges();

			Section[] dbSections = await sectionRepository.ListAsync();

			AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
			AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
		}

		[TestMethod]
		public async Task ListAsync_WhenSectionsDontExists_ThenReturnEmptyList()
		{
			Section[] sections = await sectionRepository.ListAsync();

			Assert.IsTrue(sections.Count() == 0);
		}

		[TestMethod]
		public async Task GetByIDAsync_WhenSectionExists_ThenReturnsSectionByID()
		{
			Section section = SectionProvider();

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public async Task GetByIDAsync_WhenSectionDontExists_ThenReturnsNull()
		{
			Guid id = Guid.NewGuid();

			Section section = await sectionRepository.GetByIDAsync(id);

			Assert.IsNull(section);
		}

		[TestMethod]
		public async Task GetByNameAsync_WhenSectionExists_ThenReturnsSectionByName()
		{
			Section section = SectionProvider();

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = await sectionRepository.GetByNameAsync(section.Name);

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public async Task GetByNameAsync_WhenSectionDontExists_ThenReturnsNull()
		{
			Section section = await sectionRepository.GetByNameAsync("UnexistingSection");

			Assert.IsNull(section);
		}

		[TestMethod]
		public void Create_WhenSectionDontExists_ThenAddSectionToDatabase()
		{
			Section section = SectionProvider();

			sectionRepository.Create(section);
			dbContext.SaveChanges();

			Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public void Create_WhenSectionExists_ThenThrowException()
		{
			Section section = SectionProvider();
			sectionRepository.Create(section);

			Section section2 = section;
			try
			{
				sectionRepository.Create(section2);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}
		[TestMethod]
		public void Create_WhenSectionIsNull_ThenThrowException()
		{
			Section section = null;
			try
			{
				sectionRepository.Create(section);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void Update_WhenSectionExists_ThenUpdateCurrentSection()
		{
			Section section = SectionProvider();

			sectionRepository.Create(section);
			dbContext.SaveChanges();

			var updatedName = "This is a new Name";
			section.Name = updatedName;

			sectionRepository.Update(section);
			dbContext.SaveChanges();

			section = sectionRepository.GetByIDAsync(section.ID).Result;

			Assert.AreEqual(updatedName, section.Name);
		}

		[TestMethod]
		public void Update_WhenSectionDontExists_ThenThrowException()
		{
			Section section = SectionProvider();
			section.Name = "This is a new Name";
			try
			{
				sectionRepository.Update(section);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void Update_WhenNothingChanged_ThenKeepSameSection()
		{
			Section section = SectionProvider();

			sectionRepository.Create(section);
			dbContext.SaveChanges();
			sectionRepository.Update(section);
			dbContext.SaveChanges();

			Section sameSection = sectionRepository.GetByIDAsync(section.ID).Result;

			AssertSectionEqual(section, sameSection);
		}

		[TestMethod]
		public void Delete_WhenSectionExists_ThenDeleteSection()
		{
			Section section = SectionProvider();

			sectionRepository.Create(section);
			dbContext.SaveChanges();
			sectionRepository.Delete(section);
			dbContext.SaveChanges();

			Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

			Assert.IsNull(dbSection);
		}

		[TestMethod]
		public void Delete_WhenSectionDontExists_ThenThrowException()
		{
			Section section = SectionProvider();
			try
			{
				sectionRepository.Delete(section);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void Upsert_WhenSectionDontExists_ThenAddSection()
		{
			Section section = SectionProvider();

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

			AssertSectionEqual(section, dbSection);
		}

		[TestMethod]
		public void Upsert_WhenSectionExists_ThenUpdateSection()
		{
			Section section = SectionProvider();
			sectionRepository.Create(section);
			dbContext.SaveChanges();

			var newName = "This is a new Name";
			section.Name = newName;

			sectionRepository.Upsert(section);
			dbContext.SaveChanges();

			Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

			Assert.AreEqual(newName, dbSection.Name);
		}

		private Section SectionProvider()
		{
			return new Section { ID = Guid.NewGuid(), Name = "Test Name" };
		}

		private void AssertSectionEqual(Section expected, Section actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Name, actual.Name);
		}
	}
}
