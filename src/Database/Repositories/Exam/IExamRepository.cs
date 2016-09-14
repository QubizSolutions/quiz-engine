using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
	public interface IExamRepository
	{
		void UpdateExam(Exam exam);
		void UpdateExamAnswers(ExamAnswer[] answers);
		Task<IQueryable<Exam>> GetAllExams();
		Task<Exam> GetExamByID(Guid id);
		Task<IQueryable<TestDefinition>> GetAllTestDefinitions();
		Task<TestDefinition> GetTestDefinitionByID(Guid id);
		Task<IQueryable<ExamAnswer>> GetAllExamAnswers();
		Task<IQueryable<QuestionDefinition>> GetAllQuestionDefinitions();
		Task<IQueryable<OptionDefinition>> GetAllOptionDefinitions();
		Task<IQueryable<Section.Contract.Section>> GetAllSections();
	}

	public enum SortType
	{
		Descendant = 0,
		Ascendant = 1,
		None = 2
	};

}