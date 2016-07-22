using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class TestRepository : BaseRepository<TestDefinition>, ITestRepository
    {
        public TestRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async void DeleteTest(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<TestDefinition>> GetAllTestDefinitions(Guid testID)
        {
            throw new NotImplementedException();
        }

        public async Task<TestDefinition> GetTestDefinitionByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async void UpdateTest(TestDefinition test)
        {
            throw new NotImplementedException();
        }
    }
}