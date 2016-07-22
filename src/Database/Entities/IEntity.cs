using System;
using System.ComponentModel.DataAnnotations;

namespace Qubiz.QuizEngine.Database.Entities
{
    public interface IEntity
    {
        [Key]
        Guid ID { get; set; }
    }
}
