using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Models;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Services
{
    public class QuestionService : IQuestionService
    {
        private IConfig config;

       
        UnitOfWorkFactory factory = new UnitOfWorkFactory();
        public async Task DeleteQuestionAsync(Guid id)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                await unitOfWork.QuestionRepository.DeleteQuestionAsync(id);
            }
        }

        public async Task<PagedResult<QuestionListItem>> GetQuestionsByPageAsync(int pagenumber)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                IQueryable<Database.Entities.QuestionDefinition> questions = await unitOfWork.QuestionRepository.GetQuestionsAsync();

                if (pagenumber > questions.ToList().Count / 10)
                {
                    pagenumber = questions.ToList().Count / 10;
                }

                if (pagenumber < 0)
                {
                    pagenumber = 0;
                }

                var questionsFiltered = questions.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

                Section[] sections = await unitOfWork.SectionRepository.GetAllSectionsAsync();

                return new PagedResult<QuestionListItem>
                {
                    Items = questionsFiltered.OrderBy(q => q.Number).Skip(pagenumber * 10).Take(10).Select(q => new QuestionListItem
                    {
                        ID = q.ID,
                        Number = q.Number,
                        Section = (sections.SingleOrDefault(s => s.ID == q.SectionID) ?? new Section { Name = string.Empty }).Name
                    }).ToArray(),
                    TotalCount = questionsFiltered.Count()
                };
            }
        }
    }
}