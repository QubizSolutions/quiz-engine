using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class ExamAnswer : IEntity
    {
        public Guid ID { get; set; }

        public Guid ExamID { get; set; }

        public Guid QuestionID { get; set; }

        public string AnswerString { get; set; }

        [NotMapped]
        public Guid[] Answers
        {
            get
            {
                return string.IsNullOrEmpty(AnswerString) ? null : AnswerString.Split(',').Select(s => new Guid(s)).ToArray();
            }
            set
            {
                AnswerString = value != null ? string.Join(",", value.Select(g => g.ToString())) : null;
            }
        }
    }
}