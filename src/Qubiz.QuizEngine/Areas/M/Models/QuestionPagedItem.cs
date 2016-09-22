using System;

namespace Qubiz.QuizEngine.Areas.M.Models
{
	public class QuestionPagedItem
	{
		public Guid ID { get; set; }
		public int Number { get; set; }
		public string Section { get; set; }
	}
}