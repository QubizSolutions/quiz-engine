using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Repositories.Section.Contract;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.SectionService;
using System;
using System.Threading.Tasks;

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

			unitOfWorkFactoryMock.Setup(x => x.Create()).Returns(unitOfWorkMock.Object);
			unitOfWorkMock.Setup(x => x.Dispose());
		}

		[TestCleanup]
		public void TestCleanUp()
		{
			unitOfWorkFactoryMock.VerifyAll();
			unitOfWorkMock.VerifyAll();
			sectionRepositoryMock.VerifyAll();
		}

		[TestMethod]
		public async Task DeleteSectionAsync_WhenSectionExists_ThenReturnsNoError()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));
			sectionRepositoryMock.Setup(x => x.Delete(section));
			unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(()=>Task.CompletedTask);

			ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);

			Assert.AreEqual(0, errors.Length);
		}

		[TestMethod]
		public async Task DeleteSectionAsync_WhenSectionDoesNotExist_ThenReturnsNull()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByIDAsync(section.ID)).Returns(Task.FromResult((Section)null));

			ValidationError[] errors = await sectionService.DeleteSectionAsync(section.ID);

			Assert.AreEqual(1, errors.Length);
			Assert.AreEqual("Deletion failed! There is no Section instance with this ID!", errors[0].Message);
		}

		[TestMethod]
		public async Task GetAllSectionsAsync_WhenSectionsExists_ThenReturnListOfSections()
		{
			Section[] sectionsList = new Section[]
			{
				new Section {ID = Guid.NewGuid(), Name = "Section 1" },
				new Section {ID = Guid.NewGuid(), Name = "Section 2" },
				new Section {ID = Guid.NewGuid(), Name = "Section 3" }
			};

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.ListAsync()).Returns(Task.FromResult(sectionsList));

			QuizEngine.Services.SectionService.Contract.Section[] sections = await sectionService.GetAllSectionsAsync();

			AssertAreEqual(sectionsList[0], sections[0]);
			AssertAreEqual(sectionsList[1], sections[1]);
			AssertAreEqual(sectionsList[2], sections[2]);
		}

		[TestMethod]
		public async Task AddSectionAsync_WhenAddingUnexistingSection_ThenReturnNoError()
		{
			Qubiz.QuizEngine.Services.SectionService.Contract.Section section = new Qubiz.QuizEngine.Services.SectionService.Contract.Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByNameAsync(section.Name)).Returns(Task.FromResult((Section)null));

			sectionRepositoryMock.Setup(x => x.Upsert(It.Is<Section>(s => HaveEqualState(section, s))));

			unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(()=>Task.CompletedTask);

			ValidationError[] errors = await sectionService.AddSectionAsync(section);

			Assert.AreEqual(0, errors.Length);
		}

		[TestMethod]
		public async Task AddSectionAsync_WhenAddingExistingSection_ThenReturnError()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));

			ValidationError[] error = await sectionService.AddSectionAsync(section.DeepCopyTo<QuizEngine.Services.SectionService.Contract.Section>());

			Assert.AreEqual(1, error.Length);
			Assert.AreEqual("Add failed! There already exists a Section instance with this name!", error[0].Message);
		}

		[TestMethod]
		public async Task UpdateSectionAsync_WhenUpdateExistingSection_ThenReturnsNoError()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(repository => repository.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByNameAsync(section.Name)).Returns(Task.FromResult(section));
			sectionRepositoryMock.Setup(x => x.Upsert(It.Is<Section>(s=>HaveEqualState(s,section))));
			unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(() => Task.CompletedTask);

			ValidationError[] errors = await sectionService.UpdateSectionAsync(section.DeepCopyTo<QuizEngine.Services.SectionService.Contract.Section>());

			Assert.AreEqual(0, errors.Length);
		}

		[TestMethod]
		public async Task UpdateSectionAsync_WhenUpdateUnexistingSection_ThenReturnError()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };
			Section anotherSection = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByNameAsync(section.Name)).Returns(Task.FromResult(anotherSection));

			ValidationError[] errors = await sectionService.UpdateSectionAsync(section.DeepCopyTo<QuizEngine.Services.SectionService.Contract.Section>());

			Assert.AreEqual("Update failed! There is already a Section with this name !", errors[0].Message);
			Assert.AreEqual(1, errors.Length);
		}

		[TestMethod]
		public async Task GetSectionAsync_WhenSectionExists_ThenReturnExistingSection()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Section" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByIDAsync(section.ID)).Returns(Task.FromResult(section));

			QuizEngine.Services.SectionService.Contract.Section newSection = await sectionService.GetSectionAsync(section.ID);

			AssertAreEqual(section, newSection);
		}
		[TestMethod]
		public async Task GetSectionAsync_WhenSectionDoesNotExist_ThenReturnNull()
		{
			Section section = new Section { ID = Guid.NewGuid(), Name = "Test Name" };

			unitOfWorkMock.Setup(x => x.SectionRepository).Returns(sectionRepositoryMock.Object);

			sectionRepositoryMock.Setup(x => x.GetByIDAsync(section.ID)).Returns(Task.FromResult((Section)null));

			QuizEngine.Services.SectionService.Contract.Section returnedSection = await sectionService.GetSectionAsync(section.ID);

			Assert.IsNull(returnedSection);
		}

		private void AssertAreEqual(Section expected, QuizEngine.Services.SectionService.Contract.Section actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Name, actual.Name);
		}

		private bool HaveEqualState(QuizEngine.Database.Repositories.Section.Contract.Section expected, QuizEngine.Database.Repositories.Section.Contract.Section actual)
		{
			return expected.ID == actual.ID
				&& expected.Name == actual.Name;
		}
		private bool HaveEqualState(Qubiz.QuizEngine.Services.SectionService.Contract.Section expected, Section actual)
		{
			return expected.ID == actual.ID
				&& expected.Name == actual.Name;
		}
	}
}