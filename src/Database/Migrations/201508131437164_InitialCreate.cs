namespace Qubiz.QuizEngine.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ExamAnswers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ExamID = c.Guid(nullable: false),
                        QuestionID = c.Guid(nullable: false),
                        AnswerString = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TestID = c.Guid(nullable: false),
                        CandidateName = c.String(nullable: false),
                        AllQuestions = c.Int(nullable: false),
                        TotalScore = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsEnded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OptionDefinitions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        QuestionID = c.Guid(nullable: false),
                        Answer = c.String(),
                        IsCorrectAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionDefinitions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        QuestionText = c.String(nullable: false),
                        SectionID = c.Guid(nullable: false),
                        Complexity = c.Byte(nullable: false),
                        Type = c.Int(nullable: false),
                        Number = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestSections",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TestID = c.Guid(nullable: false),
                        SectionID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestQuestions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TestID = c.Guid(nullable: false),
                        QuestionID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestDefinitions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        NumberOfQuestions = c.Int(nullable: false),
                        QuestionsSelectedRandomly = c.Boolean(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        ShowScoreWhenCompleted = c.Boolean(nullable: false),
                        MinutesAllowed = c.Int(nullable: false),
                        MinComplexity = c.Byte(),
                        MaxComplexity = c.Byte(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestDefinitions");
            DropTable("dbo.TestQuestions");
            DropTable("dbo.TestSections");
            DropTable("dbo.Sections");
            DropTable("dbo.QuestionDefinitions");
            DropTable("dbo.OptionDefinitions");
            DropTable("dbo.Exams");
            DropTable("dbo.ExamAnswers");
            DropTable("dbo.Admins");
        }
    }
}
