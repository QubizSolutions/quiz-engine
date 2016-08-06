using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Models;

namespace Qubiz.QuizEngine.Services
{
	public class QuestionService : IQuestionService
	{

		private IConfig config;

		public QuestionService(IConfig config)
		{
			this.config = config;
		}

        public async void DeleteQuestion(Guid id)
        {
            UnitOfWork unit = new UnitOfWork(config);
            unit.QuestionRepository.DeleteQuestion(id);
        }

        public async Task<PagedResult<QuestionListItem>> GetQuestionsByPage(int pagenumber)
        {
            UnitOfWork unit = new UnitOfWork(config);

            var initialQuestions = await unit.QuestionRepository.GetQuestions();

            if (pagenumber > initialQuestions.ToList().Count / 10)
            {
                pagenumber = initialQuestions.ToList().Count / 10;
            }
            if (pagenumber < 0)
            {
                pagenumber = 0;
            }


            var questionsFiltered = initialQuestions.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

            IQueryable<Section> sections = await unit.SectionRepository.GetAllSections();

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
