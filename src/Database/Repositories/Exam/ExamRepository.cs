using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async void UpdateExam(Exam exam)
        {
            throw new NotImplementedException();
        }

        public async void UpdateExamAnswers(ExamAnswer[] answers)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Exam>> GetAllExams()
        {
            throw new NotImplementedException();
        }

        public async Task<Exam> GetExamByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<TestDefinition>> GetAllTestDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<TestDefinition> GetTestDefinitionByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ExamAnswer>> GetAllExamAnswers()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<QuestionDefinition>> GetAllQuestionDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<OptionDefinition>> GetAllOptionDefinitions()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Section>> GetAllSections()
        {
            throw new NotImplementedException();
        }
    }
}