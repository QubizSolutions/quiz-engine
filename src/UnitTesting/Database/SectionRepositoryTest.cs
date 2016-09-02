using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Entities;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task ListAsync_ExistingSections_ReturnArrayOfSections()
        {
            Section section1 = new Section { ID = Guid.NewGuid(), Name = "Section1" };
            Section section2 = new Section { ID = Guid.NewGuid(), Name = "Section2" };

            sectionRepository.Upsert(section1);
            sectionRepository.Upsert(section2);

            Section[] dbSections = await sectionRepository.ListAsync();

            AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
            AssertSectionEqual(section1, dbSections.First(section => section.ID == section1.ID));
        }

        [TestMethod]
        public async Task GetByIDAsync_ExistingSection_ReturnsSectionByID()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "TestSection" };

            sectionRepository.Upsert(section);

            Section dbSection = await sectionRepository.GetByIDAsync(section.ID);

            AssertSectionEqual(section, dbSection);
        }

        [TestMethod]
        public async Task GetByNameAsync_ExistingSection_ReturnsSectionByName()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "TestSection" };

            sectionRepository.Upsert(section);

            Section dbSection = await sectionRepository.GetByNameAsync(section.Name);

            AssertSectionEqual(section, dbSection);
        }

        [TestMethod]
        public async Task ListAsync_UnexistingSections_ReturnEmptyList()
        {
            Section[] sections = await sectionRepository.ListAsync();

            Assert.IsTrue(sections.Count() == 0);
        }

        [TestMethod]
        public async Task GetByIDAsync_UnexistingSection_ReturnsNull()
        {
            Guid id = Guid.NewGuid();

            Section section = await sectionRepository.GetByIDAsync(id);

            Assert.IsNull(section);
        }

        [TestMethod]
        public async Task GetByNameAsync_UnexistingSection_ReturnsNull()
        {
            Section section = await sectionRepository.GetByNameAsync("UnexistingSection");

            Assert.IsNull(section);
        }

        private void AssertSectionEqual(Section expected, Section actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
