using AutoMapper.Runtime.Extensions;
using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Linq;

namespace Qubiz.QuizEngine.Repositories
{
    public interface ITestRepository
    {
        ViewModels.PagedResult<ViewModels.TestListItem> GetTestsFiltered(string filter, bool includeUnPublished);

        ViewModels.TestDefinitionDetail GetTestByID(Guid id);

        void UpdateTest(ViewModels.TestDefinitionDetail Test);

        void DeleteTest(Guid id);
    }

    public class TestRepository : ITestRepository
    {
		private readonly IRepository repository;

        public TestRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewModels.TestDefinitionDetail GetTestByID(Guid id)
        {
            ViewModels.TestDefinitionDetail test = repository.GetByID<TestDefinition>(id).DeepCopyTo<ViewModels.TestDefinitionDetail>();

            if (test.QuestionsSelectedRandomly)
                test.Sections = repository.All<TestSection>().Where(c => c.TestID == id).ToArray();
            else
                test.Questions = repository.All<TestQuestion>().Where(c => c.TestID == id).ToArray();

            return test;
        }

        public void UpdateTest(ViewModels.TestDefinitionDetail test)
        {
            repository.Upsert(test.DeepCopyTo<TestDefinition>());

            TestSection[] existingSections = repository.All<TestSection>().Where(o => o.TestID == test.ID).ToArray();
            TestQuestion[] existingQuestions = repository.All<TestQuestion>().Where(o => o.TestID == test.ID).ToArray();

            if (test.QuestionsSelectedRandomly)
            {
                foreach (TestSection sectionToUpdate in test.Sections)
                {
                    TestSection existingItem = existingSections.FirstOrDefault(s => s.SectionID == sectionToUpdate.SectionID);
                    if (existingItem == null)
                    {
                        sectionToUpdate.ID = Guid.NewGuid();
                        sectionToUpdate.TestID = test.ID;
                        repository.Upsert(sectionToUpdate);
                    }
                }
            }
            else
            {
                foreach (TestQuestion questionToupdate in test.Questions)
                {
                    TestQuestion existingItem = existingQuestions.FirstOrDefault(s => s.QuestionID == questionToupdate.QuestionID);
                    if (existingItem == null)
                    {
                        questionToupdate.ID = Guid.NewGuid();
                        questionToupdate.TestID = test.ID;
                        repository.Upsert(questionToupdate);
                    }
                }
            }

            foreach (TestSection item in existingSections.Where(eq => test.Sections == null || !test.Sections.Any(q => q.SectionID == eq.SectionID)))
            {
                repository.Delete(item);
            }

            foreach (TestQuestion item in existingQuestions.Where(eq => test.Questions == null || !test.Questions.Any(q => q.QuestionID == eq.QuestionID)))
            {
                repository.Delete(item);
            }

            repository.SaveChanges();
        }

        public ViewModels.PagedResult<ViewModels.TestListItem> GetTestsFiltered(string filter, bool includeUnPublished)
        {
            IQueryable<TestDefinition> tests = repository.All<TestDefinition>();

            if (!includeUnPublished)
                tests = tests.Where(q => q.IsPublished);

            if (filter != null)
                tests = tests.Where(q => q.Title.Contains(filter));

            return new ViewModels.PagedResult<ViewModels.TestListItem>
            {
                Items = tests.Take(20).ToArray().DeepCopyTo<ViewModels.TestListItem[]>(),
                TotalCount = tests.Count()
            };
        }

        public void DeleteTest(Guid id)
        {
            repository.Delete<TestDefinition>(id);

            foreach (TestSection item in repository.All<TestSection>().Where(o => o.TestID == id))
            {
                repository.Delete(item);
            }

            foreach (TestQuestion item in repository.All<TestQuestion>().Where(o => o.TestID == id))
            {
                repository.Delete(item);
            }

            repository.SaveChanges();
        }

    }
}