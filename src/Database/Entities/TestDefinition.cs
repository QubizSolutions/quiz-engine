using System;
using System.ComponentModel.DataAnnotations;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class TestDefinition : IEntity
    {
        public Guid ID { get; set; }

        [Required]
        public string Title { get; set; }

        public int NumberOfQuestions { get; set; }

        public bool QuestionsSelectedRandomly { get; set; }

        public bool IsPublished { get; set; }

        public bool ShowScoreWhenCompleted { get; set; }

        public int MinutesAllowed { get; set; }

        public byte? MinComplexity { get; set; }

        public byte? MaxComplexity { get; set; }
    }
}