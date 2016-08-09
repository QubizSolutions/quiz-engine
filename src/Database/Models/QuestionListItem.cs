using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qubiz.QuizEngine.Database.Models
{
	public class QuestionListItem
	{
		public Guid ID { get; set; }
		public int Number { get; set; }
		public string Section { get; set; }
	}
}