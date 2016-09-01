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

        private void AssertSectionEqual(Section expected, Section actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        private void AssertSectionsIsTrue(List<Section> expected, List<Section> actual)
        {
            foreach (Section expectedSection in expected)
            {
                Assert.IsTrue(actual.Any(section => section.ID == expectedSection.ID && section.Name == expectedSection.Name));
            }
        }

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
            Section section2 = new Section()
            {
                ID = Guid.NewGuid(),
                Name = "TestSection2"
            };
            Section section3 = new Section()
            {
                ID = Guid.NewGuid(),
                Name = "TestSection3"
            };

            sections.Add(section1);
            sections.Add(section2);
            sections.Add(section3);

            foreach (Section section in sections)
            {
                sectionRepository.Upsert(section);
            }

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
            List<Section> sections = sectionRepository.ListAsync().Result.ToList();
            AssertSectionsIsTrue(mockSections, sections);
        }

        [TestMethod]
        public void GetByIDAsync_ExistingSection_ReturnsSectionByID()
        {
            Section section = sectionCreate();

            Section dbSection = sectionRepository.GetByIDAsync(section.ID).Result;

            AssertSectionEqual(section, dbSection);
        }

        [TestMethod]
        public void GetByNameAsync_ExistingSection_ReturnsSectionByName()
        {
            Section section = sectionCreate();

            Section dbSection = sectionRepository.GetByNameAsync(section.Name).Result;

            AssertSectionEqual(section, dbSection);
        }
    }
}
