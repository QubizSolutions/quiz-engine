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
        
        private void AssertAdminsEqual(Admin expected,Admin actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
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

            Assert.AreEqual(2, admins.Length);

            AssertAdminsEqual(adminToAdd1, admins.First(admin =>admin.ID == adminToAdd1.ID));

            AssertAdminsEqual(adminToAdd2, admins.First(admin => admin.ID == adminToAdd2.ID));
            

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

            AssertAdminsEqual(adminToAdd, admins.First(admin => admin.ID == adminToAdd.ID));

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

            AssertAdminsEqual(adminToAdd, adminFoundById);

        }

        [TestMethod]
        public void GetAdminByNameAsync_CreateAdminAddAdmin_ReturnAdminWithGivenName()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(admin);

            Admin adminFoundByName = adminRepository.GetByNameAsync(admin.Name).Result;

            AssertAdminsEqual(admin, adminFoundByName);
        }

        
    }

}
        
            
    
