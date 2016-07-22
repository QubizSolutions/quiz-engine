using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class TestQuestionRepository : BaseRepository<TestSection>, ITestQuestionRepository
    {
		public TestQuestionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async void DeleteTestQuestions(TestQuestion[] questions)
        {
            throw new NotImplementedException();
        }

        public async Task<TestQuestion[]> GetAllTestQuestionsByTestID(Guid testID)
        {
            throw new NotImplementedException();
        }

        public async void UpdateTestQuestions(Guid testID, TestQuestion[] questions)
        {
            throw new NotImplementedException();
        }
    }
}