using System;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class TestSection : IEntity
    {
        public Guid ID { get; set; }

        public Guid TestID { get; set; }

        public Guid SectionID { get; set; } 
    }
}