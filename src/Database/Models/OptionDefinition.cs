using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Models
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
