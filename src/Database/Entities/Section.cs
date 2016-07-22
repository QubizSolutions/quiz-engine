using System;
using System.ComponentModel.DataAnnotations;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class Section: IEntity
    {
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}