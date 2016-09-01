using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.UnitTesting.Database
{
    [TestClass]
    public class AdminRepositoryTest
    {
        private IConfig config;
        private QuizEngineDataContext dbContext;
        private AdminRepository adminRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            config = new Config();

            dbContext = new QuizEngineDataContext(config.ConnectionString);

            adminRepository = new AdminRepository(dbContext, null);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM [dbo].[Admins]");
        }
        
        [TestMethod]
        public void GetListAsync_GetAdminsFromDatabase_ReturnListOfAdmins()
        {
            Admin adminToAdd1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin1"
            };
            Admin adminToAdd2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin2"
            };

            adminRepository.Upsert(adminToAdd1);
            adminRepository.Upsert(adminToAdd2);

            Admin[] admins = adminRepository.ListAsync().Result;

            int adminRepositorySize = admins.Length;
            Assert.IsTrue(adminRepositorySize == 2);

            Assert.IsTrue(admins.Any(admin => admin.ID == adminToAdd1.ID && admin.Name == adminToAdd1.Name));

            Assert.IsTrue(admins.Any(admin => admin.ID == adminToAdd2.ID && admin.Name == adminToAdd2.Name));

        }
        
        [TestMethod]
        public void AddAdminsAsync_CreateAndAddAdmin_ReturnCreatedAdmin()
        {
            Admin adminToAdd = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };
            
            adminRepository.Upsert(adminToAdd);

            Admin[] admins = adminRepository.ListAsync().Result;

            Assert.IsTrue(admins.Any(admin => admin.ID == adminToAdd.ID && admin.Name == adminToAdd.Name));

        }

        [TestMethod]
        public void GetAdminByIdAsync_CreateAndAddAdmin_ReturnAdminWithGivenId()
        {
            Admin adminToAdd = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(adminToAdd);

            Admin adminFoundById = adminRepository.GetByIDAsync(adminToAdd.ID).Result;

            Assert.IsNotNull(adminFoundById);

            Assert.IsTrue(adminFoundById.ID == adminToAdd.ID && adminFoundById.Name == adminToAdd.Name);

        }

        [TestMethod]
        public void GetAdminByNameAsync_CreateAdminAddAdmin_ReturnAdminWithGivenName()
        {
            Admin adminToAdd = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(adminToAdd);

            Admin adminFoundByName = adminRepository.GetByNameAsync(adminToAdd.Name).Result;

            Assert.IsNotNull(adminFoundByName);

            Assert.IsTrue(adminFoundByName.ID == adminToAdd.ID && adminFoundByName.Name == adminToAdd.Name);
        }

        
    }

}
        
            
    
