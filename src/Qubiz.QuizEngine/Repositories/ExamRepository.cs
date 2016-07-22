using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Qubiz.QuizEngine.Repositories
{
    public interface IExamRepository
    {
        void UpdateExam(ViewModels.ExamDetail exam);
        ViewModels.PagedResult<ExamListItem> GetExamsFiltered(string nameFilter, SortType? dateSort, SortType? scoreSort, Guid? testIDFilter, int pageNumber);
        ViewModels.ExamDetail GetExamByID(Guid id);
        ViewModels.ExamResult GetExamResult(Guid examID);
    }

    public enum SortType { Descendant = 0, Ascendant, None };

    public class ExamRepository : IExamRepository
    {
        private readonly IRepository repository;

        public ExamRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public void UpdateExam(ViewModels.ExamDetail exam)
        {
            repository.Upsert(exam.DeepCopyTo<Exam>());

            foreach (var item in exam.Answers)
            {
                repository.Upsert(item);
            }

            repository.SaveChanges();
        }

        public ViewModels.PagedResult<ExamListItem> GetExamsFiltered(string nameFilter, SortType? dateSort, SortType? scoreSort, Guid? testIDFilter, int pageNumber)
        {
            IQueryable<Exam> exams = repository.All<Exam>();
            var examsFiltredQuery = exams.Select(q => new ExamListItem { ID = q.ID, CandidateName = q.CandidateName, TestID = q.TestID, TotalScore = q.TotalScore, AllQuestions = q.AllQuestions, DateTime = q.StartDate });
            var testsQuery = repository.All<TestDefinition>();

            if (testIDFilter.HasValue)
            {
                examsFiltredQuery = examsFiltredQuery.Where(t => t.TestID == testIDFilter);
                testsQuery = testsQuery.Where(t => t.ID == testIDFilter);
            }

            var examsFiltred = examsFiltredQuery.ToArray();
            var tests = testsQuery.ToArray();

            var examsComposed =  examsFiltred.Select(q => new ViewModels.ExamListItem
            {
                ID = q.ID,
                TotalScore = q.TotalScore,
                AllQuestions = q.AllQuestions,
                CandidateName = q.CandidateName,
                Title = (tests.SingleOrDefault(s => s.ID == q.TestID) ?? new TestDefinition { Title = string.Empty }).Title,
                TestID = q.TestID,
                DateTime = q.DateTime
            });

            if(!string.IsNullOrEmpty(nameFilter))
            {
                examsComposed = examsComposed.Where(e => e.CandidateName.Contains(nameFilter));
            }

            int count = examsComposed.Count();

            if(dateSort.HasValue)
            {
                if (dateSort == SortType.Descendant) examsComposed = examsComposed.OrderByDescending(e => e.DateTime);
                else examsComposed = examsComposed.OrderBy(e => e.DateTime);
            }
            if(scoreSort.HasValue)
            {
                if (scoreSort == SortType.Descendant) examsComposed = examsComposed.OrderByDescending(e => e.TotalScore);
                else examsComposed = examsComposed.OrderBy(e => e.TotalScore);
            }

            examsComposed = examsComposed.Skip(pageNumber * 20).Take(20);

            return new PagedResult<ExamListItem>() { TotalCount = count, Items = examsComposed.ToArray() };
            
        }

        public ViewModels.ExamDetail GetExamByID(Guid id)
        {
            ViewModels.ExamDetail exam = repository.GetByID<Exam>(id).DeepCopyTo<ViewModels.ExamDetail>();
            TestDefinition[] tests = repository.All<TestDefinition>().ToArray();
            exam.Title = (tests.FirstOrDefault(s => s.ID == exam.TestID) ?? new TestDefinition { Title = string.Empty }).Title;
            exam.Answers = repository.All<ExamAnswer>().Where(o => o.ExamID == id).ToArray();
            exam.MinutesAllowed = (tests.FirstOrDefault(s => s.ID == exam.TestID) ?? new TestDefinition { MinutesAllowed = 0 }).MinutesAllowed;

            return exam;
        }

        public ViewModels.ExamResult GetExamResult(Guid examID)
        {
            ExamResult exam = repository.GetByID<Exam>(examID).DeepCopyTo<ExamResult>();

            exam.Title = repository.GetByID<TestDefinition>(exam.TestID).Title;

            ExamAnswer[] answers = repository.All<ExamAnswer>().Where(a => a.ExamID == examID).ToArray();

            Guid[] questionIDs = answers.Select(a => a.QuestionID).Distinct().ToArray();

            QuestionDefinition[] allQuestions = repository.All<QuestionDefinition>().Where(q => questionIDs.Contains(q.ID)).ToArray();

            Guid[] sectionIDs = allQuestions.Select(q => q.SectionID).Distinct().ToArray();

            OptionDefinition[] allOptions = repository.All<OptionDefinition>().Where(o => questionIDs.Contains(o.QuestionID)).ToArray();

            OptionDefinition[] correctOptions = repository.All<OptionDefinition>().Where(o => o.IsCorrectAnswer && questionIDs.Contains(o.QuestionID)).ToArray();

            Section[] allSections = repository.All<Section>().Where(s => sectionIDs.Contains(s.ID)).ToArray();

            List<AnswersPerSection> sectionAnswers = new List<AnswersPerSection>();

            foreach (var section in allSections)
            {
                List<ExamAnswers> examAnswersList = new List<ExamAnswers>();

                var questions = allQuestions.Where(q => q.SectionID == section.ID).Select(q => q);

                foreach (var question in questions)
                {
                    List<string> selectedOptions = new List<string>();

                    var answerList = answers.Where(a => a.QuestionID == question.ID).Select(a => a.Answers).Single();

                    bool answeredCorrectly = false;

                    if (answerList != null)
                    {
                        foreach (var answerId in answerList)
                        {
                            var options = allOptions.FirstOrDefault(option => option.ID == answerId && option.QuestionID == question.ID).Answer;
                            selectedOptions.Add(options);
                        }

                        answeredCorrectly = AnsweredCorrectly(correctOptions.Where(o => o.QuestionID == question.ID).Select(o => o.ID).ToArray(), 
                                               answers.Where(o => o.QuestionID == question.ID).Select(o => o.Answers).Single().ToArray());
                    }

                    var validOptions = correctOptions.Where(o => o.QuestionID == question.ID).Select(o => o.Answer);

                    examAnswersList.Add(new ExamAnswers() { Question = question, SelectedOptions = selectedOptions.ToArray(), CorrectOptions = validOptions.ToArray(), AnsweredCorrectly = answeredCorrectly });
                }

                sectionAnswers.Add(new AnswersPerSection { SectionName = section.Name, Answers = examAnswersList.ToArray() });
            }

            exam.AnswersPerSection = sectionAnswers.ToArray();

            exam.ResultsPerSection = sectionIDs.Select(sectionID => new ResultPerSection
            {
                SectionName = allSections.First(s => s.ID == sectionID).Name,
                AllQuestions = allQuestions.Count(q => q.SectionID == sectionID),
                CorrectAnswers = allQuestions.Count(q => q.SectionID == sectionID
                                                      && answers.First(a => a.QuestionID == q.ID).Answers != null
                                                      && AnsweredCorrectly(correctOptions.Where(o => o.QuestionID == q.ID).Select(o => o.ID).ToArray(), 
                                                      answers.Where(o => o.QuestionID == q.ID).Select(o => o.Answers).Single().ToArray()))
            }).ToArray();

            return exam;
        }

        private bool AnsweredCorrectly(Guid[] correctOptions, Guid[] userOptions)
        {
            if (userOptions.Length != correctOptions.Length) 
                return false;

            return !userOptions.Any(o => !correctOptions.Contains(o));
        }
    }
}