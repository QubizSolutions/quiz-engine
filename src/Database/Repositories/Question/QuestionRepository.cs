using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Qubiz.QuizEngine.Database.Repositories
{
	public class QuestionRepository : BaseRepository<QuestionDefinition>, IQuestionRepository
	{
		
		public QuestionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public async void DeleteQuestion(Guid id)
        {
			//return dbSet.Remove(id);
        }

        public async Task<Models.PagedResult<Models.QuestionListItem>> GetQuestionsByPage(int pagenumber)
		{
			if(pagenumber > dbSet.ToList().Count / 10)
			{
				pagenumber = dbSet.ToList().Count / 10;
			}
			if(pagenumber < 0)
			{
				pagenumber = 0;
			}


			var questionsFiltered = dbSet.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

			IQueryable<Section> sections = await unitOfWork.SectionRepository.GetAllSections();

			return new Models.PagedResult<Models.QuestionListItem>
			{
				Items = questionsFiltered.OrderBy(q => q.Number).Skip(pagenumber * 10).Take(10).Select(q => new Models.QuestionListItem
				{
					ID = q.ID,
					Number = q.Number,
					Section = (sections.SingleOrDefault(s => s.ID == q.SectionID) ?? new Section { Name = string.Empty }).Name
				}).ToArray(),
				TotalCount = questionsFiltered.Count()
			};
		}

        public async void UpdateQuestion(QuestionDefinition question)
        {
			 

		}



    }
}