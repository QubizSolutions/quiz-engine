using Qubiz.QuizEngine.Database.Models;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Qubiz.QuizEngine.Services.Models;

namespace Qubiz.QuizEngine.Services
{
    public class QuestionService : IQuestionService
    {
        private IConfig config;

        public QuestionService(IConfig config)
        {
            this.config = config;
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                await unitOfWork.QuestionRepository.DeleteQuestionAsync(id);
                IEnumerable<OptionDefinition> options = await unitOfWork.OptionRepository.GetAllOptionsAsync();
                unitOfWork.OptionRepository.DeleteOptionsAsync(options.Where(o => o.QuestionID == id).ToArray());
            }
        }

        public async Task<PagedResult<QuestionListItem>> GetQuestionsByPageAsync(int pageNumber, int itemsPerPage)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(config))
            {
                IEnumerable<QuestionDefinition> questions = await unitOfWork.QuestionRepository.GetQuestionsAsync();

                if (pageNumber > questions.ToList().Count / itemsPerPage)
                {
                    pageNumber = questions.ToList().Count / itemsPerPage;
                }

                if (pageNumber < 0)
                {
                    pageNumber = 0;
                }

                var questionsFiltered = questions.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

                IEnumerable<Section> sections = await unitOfWork.SectionRepository.GetAllSectionsAsync();

                return new PagedResult<QuestionListItem>
                {
                    Items = questionsFiltered.OrderBy(q => q.Number).Skip(pageNumber * itemsPerPage).Take(itemsPerPage).Select(q => new QuestionListItem
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