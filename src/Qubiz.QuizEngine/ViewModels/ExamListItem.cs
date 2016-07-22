using System;

namespace Qubiz.QuizEngine.ViewModels
{
    public class ExamListItem
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string CandidateName { get; set; }

        public int AllQuestions { get; set; }

        public int? TotalScore { get; set; }

        public DateTime DateTime { get; set; }

        public Guid TestID { get; set; }
    }
}