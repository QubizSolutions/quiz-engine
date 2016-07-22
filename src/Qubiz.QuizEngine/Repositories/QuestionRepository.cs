using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Linq;

namespace Qubiz.QuizEngine.Repositories
{
    public interface IQuestionRepository
	{
        ViewModels.PagedResult<ViewModels.QuestionListItem> GetQuestionsFiltered(Guid? sectionId, int? complexityValue, QuestionType? type, string filter, int pagenumber);

		ViewModels.QuestionDetail GetQuestionByID(Guid id);

		int UpdateQuestion(ViewModels.QuestionDetail question);

		void DeleteQuestion(Guid id);

		Section[] GetAllSections();

		void UpdateSections(Section[] sections);

		Guid[] GetQuestionIDsBySctions(Guid[] sectionIDs);

		OptionDefinition[] GetOptionsByQuestionIDs(Guid[] ids);
	}

	public class QuestionRepository : IQuestionRepository
	{
		private readonly IRepository repository;

		public QuestionRepository(IRepository repository)
		{
			this.repository = repository;
		}

		public ViewModels.QuestionDetail GetQuestionByID(Guid id)
		{
			ViewModels.QuestionDetail question = repository.GetByID<QuestionDefinition>(id).DeepCopyTo<ViewModels.QuestionDetail>();

			question.Options = repository.All<OptionDefinition>().Where(o => o.QuestionID == id).OrderBy(o => o.Order).ToArray();

			return question;
		}

		public int UpdateQuestion(ViewModels.QuestionDetail question)
		{
			repository.Upsert(question.DeepCopyTo<QuestionDefinition>());

			OptionDefinition[] existingOptions = repository.All<OptionDefinition>().Where(o => o.QuestionID == question.ID).ToArray();

			foreach (OptionDefinition option in question.Options)
			{
				option.QuestionID = question.ID;
				repository.Upsert(option);
			}

			foreach (OptionDefinition item in existingOptions.Where(eo => !question.Options.Any(no => no.ID == eo.ID)))
			{
				repository.Delete(item);
			}

			repository.SaveChanges();

			return question.Number;
		}

        public ViewModels.PagedResult<ViewModels.QuestionListItem> GetQuestionsFiltered(Guid? sectionID, int? complexityValue, QuestionType? type, string filter, int pagenumber)
		{
			IQueryable<QuestionDefinition> questions = repository.All<QuestionDefinition>();

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.Contains(","))
                {
                    string[] filters = filter.Split(',').Select(f => f.TrimStart('0')).ToArray();
                    questions = questions.Where(q => filters.Contains(q.Number.ToString()));
                }
                else if (filter.Contains("-"))
                {
                    string[] filters = filter.Split('-').Select(f => f.TrimStart('0')).ToArray();
                    if (filters.Length == 2)
                    {
                        int min, max;
                        int.TryParse(filters[0], out min);
                        if (!int.TryParse(filters[1], out max)) max = int.MaxValue;

                        questions = questions.Where(q => q.Number >= min && q.Number <= max);
                    }
                }
                else
                    questions = questions.Where(q => q.Number.ToString().Contains(filter));
            }
            
           if (sectionID.HasValue)
               questions = questions.Where(o => o.SectionID == sectionID);
       
           if (complexityValue.HasValue)
               questions = questions.Where(o => o.Complexity == complexityValue);
           
           if (type.HasValue)
               questions = questions.Where(o => o.Type == type);

		   var questionsFiltered = questions.Select(q => new { ID = q.ID, Number = q.Number, SectionID = q.SectionID }).ToArray();

		   Section[] sections = repository.All<Section>().ToArray();

           return new ViewModels.PagedResult<ViewModels.QuestionListItem>
          {
                Items = questionsFiltered.OrderBy(q => q.Number).Skip(pagenumber*20).Take(20).Select(q => new ViewModels.QuestionListItem
              {
                  ID = q.ID,
                  Number = q.Number,
                  Section = (sections.SingleOrDefault(s => s.ID == q.SectionID) ?? new Section { Name = string.Empty }).Name
              }).ToArray(),
                TotalCount = questionsFiltered.Count()
          };
		}

		public Section[] GetAllSections()
		{
			return repository.All<Section>().ToArray();
		}

		public void DeleteQuestion(Guid id)
		{
			repository.Delete<QuestionDefinition>(id);

			foreach (OptionDefinition item in repository.All<OptionDefinition>().Where(o => o.QuestionID == id))
			{
				repository.Delete(item);
			}

			repository.SaveChanges();
		}

		public void UpdateSections(Section[] sections)
		{
			Section[] existingSections = repository.All<Section>().ToArray();

			foreach (Section section in sections)
			{
				if (section.ID == Guid.Empty)
					section.ID = Guid.NewGuid();

				repository.Upsert(section);
			}

			foreach (Section item in existingSections.Where(es => !sections.Any(ns => ns.ID == es.ID)))
			{
				repository.Delete(item);
			}

			repository.SaveChanges();
		}

		public Guid[] GetQuestionIDsBySctions(Guid[] sectionIDs)
		{
			return repository.All<QuestionDefinition>().Where(q => sectionIDs.Contains(q.SectionID)).Select(q => q.ID).ToArray();
		}

		public OptionDefinition[] GetOptionsByQuestionIDs(Guid[] ids)
		{
			return repository.All<OptionDefinition>().Where(o => o.IsCorrectAnswer && ids.Contains(o.QuestionID)).ToArray();
		}       
    }
}