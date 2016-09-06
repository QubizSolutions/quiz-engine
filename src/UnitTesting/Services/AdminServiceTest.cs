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
            List<Admin> dbAdmins = new List<Admin>()
            {
                new Admin
                {
                    ID = Guid.NewGuid(),
                    Name = "Name1"
                },
                new Admin
                {
                    ID = Guid.NewGuid(),
                    Name = "Name2"
                }
            };
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.ListAsync()).Returns(Task.FromResult(dbAdmins.ToArray()));

            Admin[] admins = await adminService.GetAllAdminsAsync();

            Assert.AreEqual(admins.Length, 2);
        }

        [TestMethod]
        public async Task AddAdminAsync_NonExistingAdmins_ValidationErrorIsEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            String originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin1.Name)).Returns(Task.FromResult((Admin)null));
            adminRepositoryMock.Setup(x => x.Create(admin1));
            
            ValidationError[] validationError = await adminService.AddAdminAsync(admin1, originator);
            
            Assert.AreEqual(validationError.Length, 0);
        }

        [TestMethod]
        public async Task AddAdminAsync_NonExistingAdmin_ValidationErrorNotEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            String originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin1.Name)).Returns(Task.FromResult(admin1));

            ValidationError[] validationError = await adminService.AddAdminAsync(admin1, originator);

            Assert.AreEqual(validationError.Length, 1);
            Assert.AreEqual(validationError[0].Message, "Name already exists!");
        }

        [TestMethod]
        public async Task DeleteAdminAsync_ExistingAdmin_ValidationErrorEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            String originator = "someUser";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));
            adminRepositoryMock.Setup(x => x.Delete(admin1));

            ValidationError[] validationError = await adminService.DeleteAdminAsync(admin1.ID, originator);

            Assert.AreEqual(validationError.Length, 0);
        }

        [TestMethod]
        public async Task DeleteAdminAsync_DeleteCurrentAdmin_ValidationErrorNotEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            String originator = "QUBIZ\\Name1";
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));
            
            ValidationError[] validationError = await adminService.DeleteAdminAsync(admin1.ID, originator);

            Assert.AreEqual(validationError.Length, 1);
            Assert.AreEqual(validationError[0].Message, "Can't delete yourself");
        }

        [TestMethod]
        public async Task GetAdminAsync_ExistingAdmin_ReturnAdmin()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };
            
            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);

            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));

            Admin admin2 = await adminService.GetAdminAsync(admin1.ID);
        }

        [TestMethod]
        public async Task GetAdminAsync_NonExistingAdmin_ReturnNull()
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
        public async Task UpdateAdminAsync_ExistingAdmin_ValidationErrorEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            String originator = "someUser";

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(0));

            adminRepositoryMock.Setup(x => x.Update(admin1));
            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));
            adminRepositoryMock.Setup(x => x.GetByNameAsync(admin1.Name)).Returns(Task.FromResult(admin1));

            ValidationError[] validationError = await adminService.UpdateAdminAsync(admin1, originator);

            Assert.AreEqual(validationError.Length, 0);
        }

        [TestMethod]
        public async Task UpdateAdminAsync_ExistingAdminName_ValidationErrorNotEmpty()
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

            Assert.AreEqual(validationError.Length, 1);
            Assert.AreEqual(validationError[0].Message, "Name already exists!");
        }

        [TestMethod]
        public async Task UpdateAdminAsync_UpdateCurrentAdmin_ValidationErrorNotEmpty()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "QUBIZ\\Name1"
            };

            string originator = "QUBIZ\\Name1";

            unitOfWorkMock.Setup(x => x.AdminRepository).Returns(adminRepositoryMock.Object);
            
            adminRepositoryMock.Setup(x => x.GetByIDAsync(admin1.ID)).Returns(Task.FromResult(admin1));

            ValidationError[] validationError = await adminService.UpdateAdminAsync(admin1, originator);

            Assert.AreEqual(validationError.Length, 1);
            Assert.AreEqual(validationError[0].Message, "You cannot edit yourself!");
        }
    }
}
