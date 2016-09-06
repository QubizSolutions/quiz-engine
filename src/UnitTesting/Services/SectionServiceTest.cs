using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Services.SectionService;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using System.Linq;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Infrastructure;

namespace Qubiz.QuizEngine.UnitTesting.Services
{
    [TestClass]
    public class SectionServiceTest
    {
        private SectionService sectionService;
        private Mock<ISectionRepository> sectionRepositoryMock;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        [TestInitialize]
        public void TestInitialize()
        {
            unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>(MockBehavior.Strict);
            sectionRepositoryMock = new Mock<ISectionRepository>(MockBehavior.Strict);
            unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            sectionService = new SectionService(unitOfWorkFactoryMock.Object);
            unitOfWorkFactoryMock.Setup(x => x.Create()).Returns(unitOfWorkMock.Object);
            unitOfWorkMock.Setup(x => x.Dispose());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            sectionRepositoryMock.VerifyAll();
            unitOfWorkFactoryMock.VerifyAll();
            unitOfWorkMock.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteSectionAsync_ExistingSection_RemoveSection()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.Delete(section));

            ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);


            Assert.IsTrue(errors.Length == 0);
        }

        [TestMethod]
        public async Task DeleteSectionAsync_UnexistingSection_ThrowValidationError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };
            Section dbSection = null;

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(dbSection));

            ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);

            Assert.IsTrue(errors.Length != 0);
        }

        [TestMethod]
        public async Task GetAllSectionsAsync_ExistingSections_ReturnListOfSections()
        {
            Section[] sectionsList = new Section[]
            {
                new Section {ID = Guid.NewGuid(), Name = "Section 1" },
                new Section {ID = Guid.NewGuid(), Name = "Section 2" },
                new Section {ID = Guid.NewGuid(), Name = "Section 3" }
            };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(x => x.ListAsync()).Returns(Task.FromResult(sectionsList));

            Section[] sections = await sectionService.GetAllSectionsAsync();

            CollectionAssert.AreEqual(sectionsList, sections);
        }

        [TestMethod]
        public async Task AddSectionAsync_AddUnexistingSection_AddSection()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };
            Section dbSection = null;

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(dbSection));
            sectionRepositoryMock.Setup(method => method.Create(section));

            ValidationError[] errors = await sectionService.AddSectionAsync(section);

            Assert.IsTrue(errors.Length == 0);
        }

        [TestMethod]
        public async Task AddSectionAsync_AddExistingSection_ThrowValidationError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));

            ValidationError[] error = await sectionService.AddSectionAsync(section);

            Assert.IsTrue(error.Count() != 0);
        }

        [TestMethod]
        public async Task UpdateSectionAsync_UpdateExistingSection_UpdateSection()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.Update(section));

            ValidationError[] errors = await sectionService.UpdateSectionAsync(section);

            Assert.IsTrue(errors.Count() == 0);
        }

        [TestMethod]
        public async Task UpdateSectionAsync_UpdateUnexistingSection_ThrowValidationError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };
            Section dbSection = new Section { ID = Guid.NewGuid(), Name = "Error Test" };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(dbSection));

            ValidationError[] errors = await sectionService.UpdateSectionAsync(section);

            Assert.IsTrue(errors.Count() != 0);
        }

        [TestMethod]
        public async Task GetSectionAsync_ExistingSection_ReturnExistingSection()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));

            Section newSection = await sectionService.GetSectionAsync(section.ID);

            Assert.AreEqual(section, newSection);
        }
    }
}