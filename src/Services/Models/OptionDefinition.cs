using System;

namespace Qubiz.QuizEngine.Services.Models
{
    public class OptionDefinition
    {
        public Guid ID { get; set; }

        public int Order { get; set; }

        public Guid QuestionID { get; set; }

        public string Answer { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}