using Qubiz.QuizEngine.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class QuestionRepository : BaseRepository<Entities.QuestionDefinition>, IQuestionRepository
    {
        public QuestionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async Task<IEnumerable<QuestionDefinition>> GetQuestionsAsync()
        {
            return dbSet.Select(q => new QuestionDefinition
            {
                ID = q.ID,
                Complexity = q.Complexity,
                Number = q.Number,
                QuestionText = q.QuestionText,
                SectionID = q.SectionID,
                Type = q.Type
            }).ToList();
        }

        public async Task<QuestionDefinition> GetQuestionByIDAsync(Guid id)
        {
            return dbSet.Where(q => q.ID == id).Select(q => new QuestionDefinition
            {
                ID = q.ID,
                Complexity = q.Complexity,
                Number = q.Number,
                QuestionText = q.QuestionText,
                SectionID = q.SectionID,
                Type = q.Type
            }).ToList()[0];
        }

        public async Task UpdateQuestionAsync(QuestionDefinition question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            Entities.QuestionDefinition question = dbSet.Where(i => i.ID == id).ToList()[0];
            dbSet.Remove(question);
        }
    }
}