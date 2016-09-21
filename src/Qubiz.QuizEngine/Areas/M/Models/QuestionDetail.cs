namespace Qubiz.QuizEngine.Areas.M.Models
{
	public class QuestionDetail : QuestionDefinition
	{
		public OptionDefinition[] Options { get; set; }
	}
}