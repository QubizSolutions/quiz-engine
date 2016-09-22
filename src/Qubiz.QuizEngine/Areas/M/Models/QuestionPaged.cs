namespace Qubiz.QuizEngine.Areas.M.Models
{
	public class QuestionPaged
	{
		public int TotalCount { get; set; }

		public QuestionPagedItem[] Items { get; set; }
	}
}