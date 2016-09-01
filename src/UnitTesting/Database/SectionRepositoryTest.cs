using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Entities;
using System.Linq;

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
        public void ListAsync_ExistingSections_ReturnArrayOfSections()
        {
            Section section1 = new Section { ID = Guid.NewGuid(), Name = "Section1" };
            Section section2 = new Section { ID = Guid.NewGuid(), Name = "Section2" };

            sectionRepository.Upsert(section1);
            sectionRepository.Upsert(section2);

            Section[] dbSections = sectionRepository.ListAsync().Result;
            AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
            AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
        }

        [TestMethod]
        public void GetByIDAsync_ExistingSection_ReturnsSectionByID()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "TestSection" };

            sectionRepository.Upsert(section);

            Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

            AssertSectionEqual(section, dbSection);
        }

        [TestMethod]
        public void GetByNameAsync_ExistingSection_ReturnsSectionByName()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "TestSection" };

            sectionRepository.Upsert(section);

            Section dbSection = sectionRepository.GetByNameAsync(section.Name).Result;

            AssertSectionEqual(section, dbSection);
        }

        private void AssertSectionEqual(Section expected, Section actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public void ListAsync_UnexistingSections_ReturnEmptyList()
        {
            Section[] sections = sectionRepository.ListAsync().Result;

            Assert.IsTrue(sections.Count() == 0);
        }

        [TestMethod]
        public void GetByIDAsync_UnexistingSection_ReturnsNull()
        {
            Guid id = Guid.NewGuid();

            Section section = sectionRepository.GetByIDAsync(id).Result;

            Assert.IsNull(section);
        }

        [TestMethod]
        public void GetByNameAsync_UnexistingSection_ReturnsNull()
        {
            Section section = sectionRepository.GetByNameAsync("UnexistingSection").Result;

            Assert.IsNull(section);
        }
    }
}
