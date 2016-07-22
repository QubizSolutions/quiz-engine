using Qubiz.QuizEngine.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qubiz.QuizEngine.ViewModels
{
    [NotMapped]
    public class TestDefinitionDetail : TestDefinition
    {
        public TestSection[] Sections { get; set; }
        public TestQuestion[] Questions { get; set; }
    }

}