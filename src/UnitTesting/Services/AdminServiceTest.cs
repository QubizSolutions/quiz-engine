using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.AdminService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.UnitTesting.Services
{
    [TestClass]
    public class AdminServiceTest
    {
        private Mock<IUnitOfWorkFactory> unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IAdminRepository> adminRepositoryMock;

        private AdminService adminService;

        [TestInitialize]
        public void TestInitialize()
        {
            unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>(MockBehavior.Strict);
            unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            adminService = new AdminService(unitOfWorkFactoryMock.Object);

            unitOfWorkMock.Setup(x => x.Dispose());
            unitOfWorkFactoryMock.Setup(x => x.Create()).Returns(unitOfWorkMock.Object);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            unitOfWorkFactoryMock.VerifyAll();
            unitOfWorkMock.VerifyAll();
            adminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetAllAdminsAsync_ExistingAdmins_ReturnsAdmins()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Name1"
            },
            admin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Name2"
            };

            Admin[] dbAdmins = new Admin[]
            {
                admin1.DeepCopyTo<Admin>(),
                admin2.DeepCopyTo<Admin>()       
            };
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.ListAsync()).Returns(Task.FromResult(dbAdmins));

            Admin[] admins = await adminService.GetAllAdminsAsync();

            Assert.AreEqual(2, admins.Length);
            AssertAreAdminsEqual(admin1,admins[0]);
            AssertAreAdminsEqual(admin2, admins[1]);
        }

        [TestMethod]
        public async Task AddAdminAsync_WhenAdminWithTheSameNameDoesNotExist_ThenNoValidationErrorAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin.Name)).Returns(Task.FromResult((Admin)null));
            adminRepositoryMock.Setup(x => x.Create(admin));
            
            ValidationError[] validationError = await adminService.AddAdminAsync(admin, originator);
            
            Assert.AreEqual(0, validationError.Length);
        }

        [TestMethod]
        public async Task AddAdminAsync_WhenAdminWithTheSAmeNameExists_ThenValidationErrorsAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin.Name)).Returns(Task.FromResult(admin));

            ValidationError[] validationError = await adminService.AddAdminAsync(admin, originator);

            Assert.AreEqual(validationError.Length, 1);
            Assert.AreEqual(validationError[0].Message, "Name already exists!");
        }

        [TestMethod]
        public async Task DeleteAdminAsync_WhenAdminWithGivenIDExists_ThenNoValidationErrorsAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin.ID)).Returns(Task.FromResult(admin));
            adminRepositoryMock.Setup(x => x.Delete(admin));

            ValidationError[] validationError = await adminService.DeleteAdminAsync(admin.ID, originator);

            Assert.AreEqual(0, validationError.Length);
        }

        [TestMethod]
        public async Task DeleteAdminAsync_WhenAdminWithGivenIdIsTheCurrentAdmin_ThenValidationErrorsAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "QUBIZ\\Name1";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin.ID)).Returns(Task.FromResult(admin));
            
            ValidationError[] validationError = await adminService.DeleteAdminAsync(admin.ID, originator);

            Assert.AreEqual(1, validationError.Length);
            Assert.AreEqual("Can't delete yourself", validationError[0].Message);
        }

        [TestMethod]
        public async Task GetAdminAsync_WhenAdminWhitGivenIDExists_ThenAdminWhitGivenIDIsReturned()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));

            Admin admin2 = await adminService.GetAdminAsync(admin1.ID);

            AssertAreAdminsEqual(admin1, admin2);
        }

        [TestMethod]
        public async Task GetAdminAsync_WhenAdminWithGivenIDDoesNotExist_ThenNullIsReturned()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult((Admin)null));

            Admin admin2 = await adminService.GetAdminAsync(admin1.ID);

            Assert.IsNull(admin2);
        }

        [TestMethod]
        public async Task UpdateAdminAsync_WhenAdminExists_ThenNoValidationErrorsAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "someUser";

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.Update(admin));
            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin.ID)).Returns(Task.FromResult(admin));
            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin.Name)).Returns(Task.FromResult(admin));

            ValidationError[] validationError = await adminService.UpdateAdminAsync(admin, originator);

            Assert.AreEqual(0, validationError.Length);
        }

        [TestMethod]
        public async Task UpdateAdminAsync_WhenTryingToUpdateWithAnAlredyExistingName_ThenValidationErrorsAreReturned()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            Admin admin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "someUser";

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            
            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));
            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin1.Name)).Returns(Task.FromResult(admin2));

            ValidationError[] validationError = await adminService.UpdateAdminAsync(admin1, originator);

            Assert.AreEqual(1, validationError.Length);
            Assert.AreEqual("Name already exists!", validationError[0].Message);
        }

        [TestMethod]
        public async Task UpdateAdminAsync_WhenTryingToUpdateCurrentAdmin_ThenValidationErrorsAreReturned()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "QUBIZ\\Name1";

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            
            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin.ID)).Returns(Task.FromResult(admin));

            ValidationError[] validationError = await adminService.UpdateAdminAsync(admin, originator);

            Assert.AreEqual(1, validationError.Length);
            Assert.AreEqual("You cannot edit yourself!", validationError[0].Message);
        }

        public void AssertAreAdminsEqual(Admin expected, Admin actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}