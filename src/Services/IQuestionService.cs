using Qubiz.QuizEngine.Services.Models;
using System;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services
{
    public interface IQuestionService
    {
        Task DeleteQuestionAsync(Guid id);

        Task<PagedResult<QuestionListItem>> GetQuestionsByPageAsync(int pageNumber, int itemsPerPage);
    }
}