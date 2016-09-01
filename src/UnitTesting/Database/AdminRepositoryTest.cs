﻿using System;
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
        private QuizEngineDataContext dbContext;
        private AdminRepository adminRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            IConfig config = new Config();

            dbContext = new QuizEngineDataContext(config.ConnectionString);

            adminRepository = new AdminRepository(dbContext, null);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM [dbo].[Admins]");
        }

        [TestMethod]
        public void GetListAsync_ExistingAdminsInDatabase_ReturnListOfAdmins()
        {
            Admin admin1 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin1"
            };
            Admin admin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "Admin2"
            };

            adminRepository.Upsert(admin1);
            adminRepository.Upsert(admin2);

            Admin[] admins = adminRepository.ListAsync().Result;

            Assert.AreEqual(2, admins.Length);

            AssertAdminsEqual(admin1, admins.First(a => a.ID == admin1.ID));
            AssertAdminsEqual(admin2, admins.First(a => a.ID == admin2.ID));
        }
        
        [TestMethod]
        public void AddAdminsAsync_ExistingAdmin_ReturnCreatedAdmin()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };
            
            adminRepository.Upsert(admin);

            Admin[] admins = adminRepository.ListAsync().Result;

            AssertAdminsEqual(admin, admins.First(a => a.ID == admin.ID));
        }

        [TestMethod]
        public void GetAdminByIdAsync_ExistingAdmin_ReturnAdminWithGivenId()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(admin);

            Admin dbAdmin = adminRepository.GetByIDAsync(admin.ID).Result;

            AssertAdminsEqual(admin, dbAdmin);
        }

        [TestMethod]
        public void GetAdminByIdAsync_ExistingAdmin_ReturnNull()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            Admin admin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(admin);

            Admin dbAdmin = adminRepository.GetByIDAsync(admin2.ID).Result;

            Assert.IsNull(dbAdmin);
        }

        [TestMethod]
        public void GetAdminByNameAsync_ExistingAdmin_ReturnAdminWithGivenName()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            adminRepository.Upsert(admin);

            Admin dbAdmin = adminRepository.GetByNameAsync(admin.Name).Result;

            AssertAdminsEqual(admin, dbAdmin);
        }

        [TestMethod]
        public void GetAdminByNameAsync_ExistingAdmin_ReturnNull()
        {
            Admin admin = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "AddAdminTestAdmin"
            };

            Admin admin2 = new Admin
            {
                ID = Guid.NewGuid(),
                Name = "ThisShouldBeNull"
            };

            adminRepository.Upsert(admin);

            Admin dbAdmin = adminRepository.GetByNameAsync(admin2.Name).Result;

            Assert.IsNull(dbAdmin);
        }

        private void AssertAdminsEqual(Admin expected, Admin actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}