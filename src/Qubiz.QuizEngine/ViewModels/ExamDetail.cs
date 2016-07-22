using Qubiz.QuizEngine.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qubiz.QuizEngine.ViewModels
{
    [NotMapped]
    public class ExamDetail: Exam
    {
        public ExamAnswer[] Answers { get; set; }

        public string Title { get; set; }

        public int MinutesAllowed { get; set; }
    }

    [NotMapped]
    public class ExamResult : Exam
    {
        public string Title { get; set; }

        public ResultPerSection[] ResultsPerSection { get; set; }

        public AnswersPerSection[] AnswersPerSection { get; set; }
    }

    [NotMapped]
    public class ResultPerSection
    {
        public string SectionName { get; set; }
        public int AllQuestions { get; set; }
        public int CorrectAnswers { get; set; }
    }

    public class ExamAnswers
    {
        public QuestionDefinition Question { get; set; }
        public string[] SelectedOptions { get; set; }
        public string[] CorrectOptions { get; set; }
        public bool AnsweredCorrectly { get; set; }
    }

    public class AnswersPerSection
    {
        public string SectionName { get; set; }
        public ExamAnswers[] Answers { get; set; }
    }
}