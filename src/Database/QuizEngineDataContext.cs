using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using System.Data.Entity;

namespace Qubiz.QuizEngine.Database
{
    public class QuizEngineDataContext : DbContext
    {
        public QuizEngineDataContext(string connectionString)
            : base(connectionString)
        {
#if DEBUG
            this.Database.Log = (msg) => { System.Diagnostics.Debug.WriteLine(msg); };
#endif
        }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<QuestionDefinition> Questions { get; set; }
        public DbSet<OptionDefinition> Options { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamAnswer> Anwers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<TestDefinition> Tests { get; set; }
        public DbSet<TestSection> TestGenerationDetails { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
		public DbSet<FeatureFlag> FeatureFlags { get; set; }
    }
}