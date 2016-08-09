﻿using Qubiz.QuizEngine.Database.Models;
using Qubiz.QuizEngine.Database.Repositories;
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
                IEnumerable<OptionDefinition> options = await unitOfWork.OptionRepository.GetOptionsByQuestionIDAsync(id);
                unitOfWork.OptionRepository.DeleteOptionsAsync(options.Where(o => o.QuestionID == id).ToArray());
                await unitOfWork.SaveAsync();
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

                IEnumerable<Qubiz.QuizEngine.Database.Entities.Section> sections = await unitOfWork.SectionRepository.GetAllSectionsAsync();

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