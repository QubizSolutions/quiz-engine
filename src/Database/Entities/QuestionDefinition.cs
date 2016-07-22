using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class QuestionDefinition:IEntity
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public Guid SectionID { get; set; }

        [Required]
        public byte Complexity { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
    }

    public enum QuestionType
    {
        SingleSelect, MultiSelect
    }
}