using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services
{
    public interface IQuestionService
    {
        Task DeleteQuestionAsync(Guid id);

        Task<Database.Models.PagedResult<Database.Models.QuestionListItem>> GetQuestionsByPageAsync(int pagenumber);
    }
}