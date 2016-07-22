using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Qubiz.QuizEngine.Core
{
    public interface IExamService
    {
        ViewModels.ExamDetail CreateExam(ViewModels.TestDefinitionDetail test, string candidateName);
        void EvaluateAnswers(ViewModels.ExamDetail initialExam, ViewModels.ExamDetail submitedExam);
    }

    public class ExamService : IExamService
    {
        private readonly IQuestionRepository questionRepository;

        public ExamService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public ViewModels.ExamDetail CreateExam(ViewModels.TestDefinitionDetail test, string candidateName)
        {
            Random rnd = new Random();

            Guid[] questionsSelected;

            if (test.QuestionsSelectedRandomly)
            {
                int[] noOfQuestionsPerSection = GetTheNumberOfQuestionsPerSection(test);
                questionsSelected = GetRandomQuestionsBySection(test, noOfQuestionsPerSection);
            }
            else
            {
                questionsSelected = test.Questions.Select(q => q.QuestionID).ToArray();
            }

            ViewModels.ExamDetail exam = new ViewModels.ExamDetail
            {
                ID = Guid.NewGuid(),
                AllQuestions = test.NumberOfQuestions,
                StartDate = DateTime.Now.AddSeconds(5),
                EndDate = DateTime.Now.AddSeconds(5).AddMinutes(test.MinutesAllowed),
                CandidateName = candidateName,
                TestID = test.ID,
                IsEnded = false                
            };

            exam.Answers = questionsSelected.OrderBy(q => rnd.Next()).Select(q => new ExamAnswer
            {
                ID = Guid.NewGuid(),
                ExamID = exam.ID,
                QuestionID = q
            }).ToArray();

            exam.AllQuestions = exam.Answers.Length;
            return exam;
        }

        private Guid[] GetRandomQuestionsBySection(ViewModels.TestDefinitionDetail test, int[] noOfQuestionsPerSection)
        {
            List<Guid> result = new List<Guid>();
            Random rnd = new Random();

            for (int i = 0; i < noOfQuestionsPerSection.Length; i++)
            {
                Guid[] questionsOfASection = questionRepository.GetQuestionIDsBySctions(new Guid[] { test.Sections[i].SectionID }).ToArray();
                result.AddRange(questionsOfASection.OrderBy(q => rnd.Next()).Take(noOfQuestionsPerSection[i]).ToList());
            }

            return result.ToArray();
        }

        private int[] GetTheNumberOfQuestionsPerSection(ViewModels.TestDefinitionDetail test)
        {
            int[] noOfQuestionsInASection = new int[test.Sections.Count()];
            for (int i = 0; i < test.Sections.Count(); i++) noOfQuestionsInASection[i] = questionRepository.GetQuestionIDsBySctions(new Guid[] { test.Sections[i].SectionID }).ToArray().Count();

            int[] result = new int[test.Sections.Count()];
            int noOfQuestionsToDistribute = test.NumberOfQuestions;

            while (noOfQuestionsToDistribute > 0)
            {
                bool resultUpdated = false;

                for (int i = 0; i < result.Length && noOfQuestionsToDistribute > 0; i++)
                {
                    if (noOfQuestionsInASection[i] > 0)
                    {
                        result[i]++;
                        noOfQuestionsInASection[i]--;
                        resultUpdated = true;
                        noOfQuestionsToDistribute--;
                    }
                }

                if (!resultUpdated) break;
            }

            return result;
        }

        public void EvaluateAnswers(ViewModels.ExamDetail initialExam, ViewModels.ExamDetail submitedExam)
        {
            OptionDefinition[] options = questionRepository.GetOptionsByQuestionIDs(initialExam.Answers.Select(a => a.QuestionID).ToArray());

            foreach (ExamAnswer answer in initialExam.Answers)
            {
                answer.Answers = submitedExam.Answers.First(a => a.QuestionID == answer.QuestionID).Answers;
            }

            initialExam.TotalScore = submitedExam.Answers.Count(optionAnswer => optionAnswer.Answers != null && optionAnswer.Answers.Length == options.Count(o => o.QuestionID == optionAnswer.QuestionID) && options.Where(o => o.QuestionID == optionAnswer.QuestionID).All(o => optionAnswer.Answers.Contains(o.ID)));
        }
    }
}