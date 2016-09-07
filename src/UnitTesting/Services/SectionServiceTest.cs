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
        private Mock<ISectionRepository> sectionRepositoryMock;
        private Mock<IUnitOfWorkFactory> unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private SectionService sectionService;

        [TestInitialize]
        public void TestInitialize()
        {
            unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>(MockBehavior.Strict);
            unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            sectionRepositoryMock = new Mock<ISectionRepository>(MockBehavior.Strict);

            sectionService = new SectionService(unitOfWorkFactoryMock.Object);

            unitOfWorkFactoryMock.Setup(method => method.Create()).Returns(unitOfWorkMock.Object);
            unitOfWorkMock.Setup(method => method.Dispose());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            unitOfWorkFactoryMock.VerifyAll();
            unitOfWorkMock.VerifyAll();
            sectionRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteSectionAsync_ExistingSection_ReturnsNoError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.Delete(section));
            
            unitOfWorkMock.Setup(method => method.SaveAsync()).Returns(Task.CompletedTask);
            
            ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);

            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public async Task DeleteSectionAsync_UnexistingSection_ReturnsError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult((Section)null));

            ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);

            Assert.AreEqual(1,errors.Count());
            Assert.AreEqual("Deletion failed! There is no Section instance with this ID!", errors[0].Message);

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

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.ListAsync()).Returns(Task.FromResult(sectionsList));

            Section[] sections = await sectionService.GetAllSectionsAsync();

            CollectionAssert.AreEqual(sectionsList, sections);
        }

        [TestMethod]
        public async Task AddSectionAsync_AddUnexistingSection_ReturnNoError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult((Section)null));
            sectionRepositoryMock.Setup(method => method.Create(section));

            unitOfWorkMock.Setup(method => method.SaveAsync()).Returns(Task.CompletedTask);

            ValidationError[] errors = await sectionService.AddSectionAsync(section);

            Assert.AreEqual(errors.Length, 0);
        }

        [TestMethod]
        public async Task AddSectionAsync_AddExistingSection_ThrowValidationError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));

            ValidationError[] error = await sectionService.AddSectionAsync(section);

            Assert.AreEqual(error.Count(), 1);
            Assert.AreEqual("Add failed! There already exists a Section instance with this name!", error[0].Message);
        }

        [TestMethod]
        public async Task UpdateSectionAsync_UpdateExistingSection_ReturnsNoError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));
            sectionRepositoryMock.Setup(method => method.Update(section));

            unitOfWorkMock.Setup(method => method.SaveAsync()).Returns(Task.CompletedTask);

            ValidationError[] errors = await sectionService.UpdateSectionAsync(section);

            Assert.IsTrue(errors.Count() == 0);
        }

        [TestMethod]
        public async Task UpdateSectionAsync_UpdateUnexistingSection_ReturnValidationError()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };
            Section dbSection = new Section { ID = Guid.NewGuid(), Name = "Error Test" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByNameAsync(section.Name)).Returns(Task.FromResult(dbSection));

            ValidationError[] errors = await sectionService.UpdateSectionAsync(section);

            Assert.IsTrue(errors.Count() != 0);
        }

        [TestMethod]
        public async Task GetSectionAsync_ExistingSection_ReturnExistingSection()
        {
            Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

            unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

            sectionRepositoryMock.Setup(method => method.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));

            Section newSection = await sectionService.GetSectionAsync(section.ID);

            Assert.AreEqual(section, newSection);
        }
    }
}