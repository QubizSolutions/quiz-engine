using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.UnitTesting.Database
{
    [TestClass]
    public class FeatureFlagRepositoryTest
    {
        private IConfig config;
        private QuizEngineDataContext dbContext;

        private FeatureFlagRepository featureFlagRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            config = new Config();

            dbContext = new QuizEngineDataContext(config.ConnectionString);

            featureFlagRepository = new FeatureFlagRepository(dbContext, null);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM [dbo].[FeatureFlags]");
        }

        [TestMethod]
        public void GetAllFeatureFlags_ExistingFeatureFlags_ReturnArrayOfFeatureFlags()
        {
            FeatureFlag newFeatureFlag1 = new FeatureFlag
            {
                ID = Guid.NewGuid(),
                Name = "Flag1",
                Status = true
            };

            FeatureFlag newFeatureFlag2 = new FeatureFlag
            {
                ID = Guid.NewGuid(),
                Name = "Flag2",
                Status = false
            };

            featureFlagRepository.Upsert(newFeatureFlag1);
            featureFlagRepository.Upsert(newFeatureFlag2);
           
            FeatureFlag[] featureFlags = featureFlagRepository.GetAllFeatureFlags().Result;

            Assert.IsTrue(featureFlags.Any(x => x.ID == newFeatureFlag1.ID &&
                                                x.Name == newFeatureFlag1.Name &&
                                                x.Status == newFeatureFlag1.Status));

            Assert.IsTrue(featureFlags.Any(x => x.ID == newFeatureFlag2.ID &&
                                                x.Name == newFeatureFlag2.Name &&
                                                x.Status == newFeatureFlag2.Status));
        }

        [TestMethod]
        public void GetFeatureFlagByID_ExistingFeatureFlag_ReturnsFeatureFlag()
        {
            Guid featureFlagID = Guid.NewGuid();
            FeatureFlag newFeatureFlag = new FeatureFlag
            {
                ID = featureFlagID,
                Name = "Flag1",
                Status = true
            };
             
            featureFlagRepository.Upsert(newFeatureFlag);

            FeatureFlag featureFlag = featureFlagRepository.GetFeatureFlagByID(featureFlagID).Result;
            
            Assert.AreEqual(newFeatureFlag.ID, featureFlag.ID);
            Assert.AreEqual(newFeatureFlag.Name, featureFlag.Name);
            Assert.AreEqual(newFeatureFlag.Status, featureFlag.Status);
        }

        [TestMethod]
        public void GetFeatureFlagByID_UnexistingFeatureFlag_ReturnsNull()
        {
            Guid featureFlagID = Guid.NewGuid();
            
            FeatureFlag featureFlag = featureFlagRepository.GetFeatureFlagByID(featureFlagID).Result;

            Assert.IsNull(featureFlag);
        }
    }
}
