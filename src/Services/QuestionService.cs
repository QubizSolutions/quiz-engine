using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Qubiz.QuizEngine.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public QuestionService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                await unitOfWork.QuestionRepository.DeleteQuestionAsync(id);
                IEnumerable<Database.Models.OptionDefinition> options = await unitOfWork.OptionRepository.GetOptionsByQuestionIDAsync(id);
                unitOfWork.OptionRepository.DeleteOptionsAsync(options.ToArray());
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<QuestionDetail> GetQuestionByID(Guid id)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                //List<Database.Models.QuestionDefinition> list = new List<Database.Models.QuestionDefinition>();
                QuestionDetail question = (await unitOfWork.QuestionRepository.GetQuestionByIDAsync(id)).DeepCopyTo<QuestionDetail>();
                IEnumerable<Database.Models.OptionDefinition> options = await unitOfWork.OptionRepository.GetOptionsByQuestionIDAsync(id);
                question.Options = options.Select(o => new OptionDefinition
                {
                    Answer = o.Answer,
                    ID = o.ID,
                    IsCorrectAnswer = o.IsCorrectAnswer,
                    Order = o.Order,
                    QuestionID = o.QuestionID
                }).ToArray();
                return question;
            }
        }

        public async Task UpdateQuestionAsync(QuestionDetail question)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                await unitOfWork.QuestionRepository.UpdateQuestionAsync(question.DeepCopyTo<Database.Models.QuestionDefinition>());
                unitOfWork.OptionRepository.DeleteOptionsAsync((await unitOfWork.OptionRepository.GetOptionsByQuestionIDAsync(question.ID)).ToArray());
                unitOfWork.OptionRepository.AddOptionsAsync(question.Options.Select(o => new Database.Models.OptionDefinition
                {
                    Answer = o.Answer,
                    ID = o.ID,
                    IsCorrectAnswer = o.IsCorrectAnswer,
                    Order = o.Order,
                    QuestionID = o.QuestionID
                }).ToArray());
                await unitOfWork.SaveAsync();
            }
        }

        public async Task AddQuestionAsync(QuestionDetail question)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                await unitOfWork.QuestionRepository.AddQuestionAsync(question.DeepCopyTo<Database.Models.QuestionDefinition>());
                unitOfWork.OptionRepository.AddOptionsAsync(question.Options.Select(o => new Database.Models.OptionDefinition
                {
                    Answer = o.Answer,
                    ID = o.ID,
                    IsCorrectAnswer = o.IsCorrectAnswer,
                    Order = o.Order,
                    QuestionID = o.QuestionID
                }).ToArray());
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<PagedResult<QuestionListItem>> GetQuestionsByPageAsync(int pageNumber, int itemsPerPage)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                IEnumerable<Database.Models.QuestionDefinition> questions = await unitOfWork.QuestionRepository.GetQuestionsAsync();

                if (pageNumber > questions.ToList().Count / itemsPerPage)
                {
                    pageNumber = questions.ToList().Count / itemsPerPage;
                }

                if (pageNumber < 0)
                {
                    pageNumber = 0;
                }

                var questionsFiltered = questions.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

                IEnumerable<Qubiz.QuizEngine.Database.Entities.Section> sections = await unitOfWork.SectionRepository.ListAsync();

                return new PagedResult<QuestionListItem>
                {
                    Items = questionsFiltered.OrderBy(q => q.Number).Skip(pageNumber * itemsPerPage).Take(itemsPerPage).Select(q => new QuestionListItem
                    {
                        ID = q.ID,
                        Number = q.Number,
                        Section = (sections.SingleOrDefault(s => s.ID == q.SectionID) ?? new Qubiz.QuizEngine.Database.Entities.Section { Name = string.Empty }).Name
                    }).ToArray(),
                    TotalCount = questionsFiltered.Count()
                };
            }
        }
    }
}