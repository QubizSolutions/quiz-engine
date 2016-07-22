using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface ITestSectionRepository
    {
        Task<TestSection[]> GetAllTestSectionsByTestID(Guid testID);
        void UpdateTestSections(Guid testID, TestSection[] sections);
        void DeleteTestSections(TestSection[] sections);
    }
}