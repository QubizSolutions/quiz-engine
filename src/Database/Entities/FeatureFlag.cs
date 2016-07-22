using System;

namespace Qubiz.QuizEngine.Database.Entities
{
    public class FeatureFlag : IEntity
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public bool Status { get; set; }
	}
}