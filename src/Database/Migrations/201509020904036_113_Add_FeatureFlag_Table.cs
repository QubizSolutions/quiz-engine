namespace Qubiz.QuizEngine.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class _113_Add_FeatureFlag_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeatureFlags",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeatureFlags");
        }
    }
}
