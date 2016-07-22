using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Database;

namespace Qubiz.QuizEngine.UnitTesting
{
    [TestClass]
    public class Startup
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            Database.Config config = new Database.Config();

            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<QuizEngineDataContext, Migrations.Configuration>(true));

            System.Data.Entity.DbContext dbContext = new QuizEngineDataContext(config.ConnectionString);
            dbContext.Database.Initialize(true);

            System.Data.Entity.Database.SetInitializer<QuizEngineDataContext>(null);
        }
    }
}
