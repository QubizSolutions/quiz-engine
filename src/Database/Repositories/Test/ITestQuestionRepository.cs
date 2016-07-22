using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface ITestQuestionRepository
    {
        Task<TestQuestion[]> GetAllTestQuestionsByTestID(Guid testID);
        void UpdateTestQuestions(Guid testID, TestQuestion[] questions);
        void DeleteTestQuestions(TestQuestion[] questions);
    }
}