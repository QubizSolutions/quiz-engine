using System;
using System.ComponentModel.DataAnnotations;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class Exam : IEntity
    {
        public Guid ID { get; set; }

        public Guid TestID { get; set; }

        [Required]
        public string CandidateName { get; set; }

        public int AllQuestions { get; set; }
        public int TotalScore { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsEnded { get; set; }
    }


}