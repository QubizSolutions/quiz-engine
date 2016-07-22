using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface ITestRepository
    {
        Task<TestDefinition> GetTestDefinitionByID(Guid id);
        Task<IQueryable<TestDefinition>> GetAllTestDefinitions(Guid testID);
        void UpdateTest(TestDefinition test);
        void DeleteTest(Guid id);
    }
}