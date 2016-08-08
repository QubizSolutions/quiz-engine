using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.Database.Models
{
	public class QuestionDefinition
	{
        public Guid ID { get; set; }
        
        public string QuestionText { get; set; }
        
        public Guid SectionID { get; set; }
        
        public byte Complexity { get; set; }
        
        public QuestionType Type { get; set; }
        
        public int Number { get; set; }
    }

}