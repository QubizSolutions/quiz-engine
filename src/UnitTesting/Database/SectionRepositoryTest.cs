using System;
using System.Text;
using System.Collections.Generic;
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
        private IConfig config;
        private QuizEngineDataContext dbContext;
        private SectionRepository sectionRepository;

        private Section sectionCreate()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "TestSection" };
            sectionRepository.Upsert(section);
            return section;
        }

        private List<Section> sectionsListCreate()
        {
            List<Section> sections = new List<Section>(); 
            Section section1 = new Section()
            {
                ID = Guid.NewGuid(),
                Name = "TestSection1"
            };
            sectionRepository.Upsert(section1);
            Section section2 = new Section()
            {
                ID = Guid.NewGuid(),
                Name = "TestSection2"
            };
            sectionRepository.Upsert(section2);
            Section section3 = new Section()
            {
                ID = Guid.NewGuid(),
                Name = "TestSection3"
            };
            sectionRepository.Upsert(section3);
            sections.Add(section1);
            sections.Add(section2);
            sections.Add(section3);

            return sections;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            config = new Config();
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
            List<Section> mockSections = sectionsListCreate();
            Section[] sections = sectionRepository.ListAsync().Result;
            foreach (Section section in mockSections)
            {
                Assert.IsTrue(sections.Any(x => x.ID == section.ID && x.Name == section.Name));
            }
        }

        [TestMethod]
        public void GetByIDAsync_ExistingSection_ReturnsSectionByID()
        {
            Section section = sectionCreate();

            Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

            Assert.AreEqual(section.ID, dbSection.ID);
            Assert.AreEqual(section.Name, dbSection.Name);
        }

        [TestMethod]
        public void GetByNameAsync_ExistingSection_ReturnsSectionByName()
        {
            Section section = sectionCreate();

            sectionRepository.Upsert(section);

            Section dbSection = sectionRepository.GetByNameAsync(section.Name).Result;

            Assert.AreEqual(section.ID, dbSection.ID);
            Assert.AreEqual(section.Name, dbSection.Name);
        }
    }
}
