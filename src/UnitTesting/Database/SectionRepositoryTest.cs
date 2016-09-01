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

            return section;
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

        public void GetAllSectionsAsync_ExistingSections_ReturnArrayOfSections()
        {
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

            sectionRepository.Upsert(section1);
            sectionRepository.Upsert(section2);

            Section[] sections = sectionRepository.GetAllSectionsAsync().Result;
            Assert.IsTrue(sections.Any(x => x.ID == section1.ID && x.Name == section1.Name));
            Assert.IsTrue(sections.Any(x => x.ID == section1.ID && x.Name == section1.Name));
        }

        [TestMethod]
        public void GetSectionByIDAsync_ExistingSection_ReturnsSectionByID()
        {
            var section = sectionCreate();

            sectionRepository.Upsert(section);

            var dbSection = sectionRepository.GetSectionByIDAsync(section.ID).Result;

            Assert.AreEqual(section.ID, dbSection.ID);
            Assert.AreEqual(section.Name, dbSection.Name);
        }

        [TestMethod]
        public void GetSectionByNameAsync_ExistingSection_ReturnsSectionByName()
        {
            var section = sectionCreate();

            sectionRepository.Upsert(section);

            var dbSection = sectionRepository.GetSectionByNameAsync(section.Name).Result;

            Assert.AreEqual(section.ID, dbSection.ID);
            Assert.AreEqual(section.Name, dbSection.Name);
        }
    }
}
