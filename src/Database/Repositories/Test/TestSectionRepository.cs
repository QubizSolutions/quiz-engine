using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class TestSectionRepository : BaseRepository<TestSection>, ITestSectionRepository
    {
		public TestSectionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async void DeleteTestSections(TestSection[] sections)
        {
            throw new NotImplementedException();
        }

        public async Task<TestSection[]> GetAllTestSectionsByTestID(Guid testID)
        {
            throw new NotImplementedException();
        }

        public async void UpdateTestSections(Guid testID, TestSection[] sections)
        {
            throw new NotImplementedException();
        }
    }
}