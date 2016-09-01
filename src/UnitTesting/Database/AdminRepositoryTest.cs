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
        public async Task GetAllAdminsAsync_GetAdminsFromDatabase_ReturnListOfAdmins()
        {
            /*
             *  Scenario with 2 Admins
             */

            Admin newAdmin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin1"
            };
            Admin newAdmin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin2"
            };

            adminRepository.Upsert(newAdmin1);
            adminRepository.Upsert(newAdmin2);

            Admin[] admins = await adminRepository.ListAsync();

            int adminRepositorySize = admins.Length;
            Assert.IsTrue(adminRepositorySize == 2);

            Assert.IsTrue(admins.Any(admin => admin.ID == newAdmin1.ID && admin.Name == newAdmin1.Name));

            Assert.IsTrue(admins.Any(admin => admin.ID == newAdmin2.ID && admin.Name == newAdmin2.Name));

        }

        [TestMethod]
        public async Task GetAllAdminsAsync_GetAdminsFromDatabase_ReturnListOfAdmins2()
        {
            /*
             *  Scenario with more than 2 Admins
             */

            List<Admin> inMemoryAdmins = new List<Admin>();

            for(int i = 0; i < 100; i++)
            {
                Admin newAdmin = new Admin
                {
                    ID = Guid.NewGuid(),
                    Name = "Admin1" + i.ToString()
                };

                inMemoryAdmins.Add(newAdmin);

                adminRepository.Upsert(newAdmin);

            }


            Admin[] adminsFromDb = await adminRepository.ListAsync();

            foreach(Admin adminFromMemory in inMemoryAdmins)
            {
                Assert.IsTrue(adminsFromDb.Any(admin => admin.ID == adminFromMemory.ID && admin.Name == adminFromMemory.Name));
            }

        }

        [TestMethod]
        public async Task AddAdminsAsync_CreateAndAddAdmin_ReturnCreatedAdmin()
        {
            Admin newAdmin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };
            
            adminRepository.Upsert(newAdmin1);

            Admin[] admins = await adminRepository.ListAsync();

            Assert.IsTrue(admins.Any(admin => admin.ID == newAdmin1.ID && admin.Name == newAdmin1.Name));

        }

        [TestMethod]
        public async Task GetAdminByIdAsync_CreateAndAddAdmin_ReturnAdminWithGivenId()
        {
            /*
             * Scenario with only one Admin in the database
             */

            Admin newAdmin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(newAdmin1);

            Admin adminFoundById = await adminRepository.GetByIDAsync(newAdmin1.ID);

            Assert.IsNotNull(adminFoundById);

            Assert.IsTrue(adminFoundById.ID == newAdmin1.ID && adminFoundById.Name == newAdmin1.Name);

        }

        [TestMethod]
        public async Task GetAdminByIdAsync_CreateAndAddAdmin_ReturnAdminWithGivenId2()
        {
            /*
             * Scenario with more than one Admin in the database
             */

            Admin newAdmin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(newAdmin);


            for(int i = 0; i < 100; i++)
            {
                Admin newAdmin1 = new Admin
                {
                    ID = Guid.NewGuid(),
                    Name = "AddAdminTestAdmin" + i.ToString()
                };

                adminRepository.Upsert(newAdmin1);

            }

            Admin adminFoundById = await adminRepository.GetByIDAsync(newAdmin.ID);

            Assert.IsNotNull(adminFoundById);

            Assert.IsTrue(adminFoundById.ID == newAdmin.ID && adminFoundById.Name == newAdmin.Name);

        }

        [TestMethod]
        public async Task GetAdminByNameAsync_CreateAdminAddAdmin_ReturnAdminWithGivenName()
        {
            /*
             * Scenario with only one Admin in the database
             */

            Admin newAdmin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(newAdmin1);

            Admin adminFoundByName = await adminRepository.GetByNameAsync(newAdmin1.Name);

            Assert.IsNotNull(adminFoundByName);

            Assert.IsTrue(adminFoundByName.ID == newAdmin1.ID && adminFoundByName.Name == newAdmin1.Name);
        }

        [TestMethod]
        public async Task GetAdminByNameAsync_CreateAdminAddAdmin_ReturnAdminWithGivenName2()
        {
            /*
             * Scenario with more than only one Admin in the database
             */

            Admin newAdmin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(newAdmin);

            for (int i = 0; i < 100; i++)
            {
                Admin newAdmin1 = new Admin
                {
                    ID = Guid.NewGuid(),
                    Name = "AddAdminTestAdmin" + i.ToString()
                };

                adminRepository.Upsert(newAdmin1);

            }

            Admin adminFoundById = await adminRepository.GetByNameAsync(newAdmin.Name);

            Assert.IsNotNull(adminFoundById);

            Assert.IsTrue(adminFoundById.ID == newAdmin.ID && adminFoundById.Name == newAdmin.Name);
        }
    }

}
        
            
    
