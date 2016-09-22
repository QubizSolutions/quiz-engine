using System;

namespace Qubiz.QuizEngine.Areas.M.Models
{
	public class Option
	{
		public Guid ID { get; set; }

		public int Order { get; set; }

		public Guid QuestionID { get; set; }

		public string Answer { get; set; }

		public bool IsCorrectAnswer { get; set; }
	}
}