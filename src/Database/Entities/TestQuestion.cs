using System;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class TestQuestion : IEntity
    {
        public Guid ID { get; set; }

        public Guid TestID { get; set; }

        public Guid QuestionID { get; set; }
    }
}