namespace Qubiz.QuizEngine.Migrations
{
    using Database;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<QuizEngineDataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			ContextKey = "Qubiz.QuizEngine.QuizEngineDataContext";
		}

		protected override void Seed(QuizEngineDataContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}
}
