using Qubiz.QuizEngine.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qubiz.QuizEngine.ViewModels
{
    [NotMapped]
    public class QuestionDetail : QuestionDefinition
    {
        public OptionDefinition[] Options { get; set; }
    }
}