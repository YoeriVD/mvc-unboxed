namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Instances : DbMigration
    {
        public override void Up()
        {
            AddColumn("Execution.Question", "Question_Id", c => c.Int());
            CreateIndex("Execution.Question", "Question_Id");
            AddForeignKey("Execution.Question", "Question_Id", "Definition.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Execution.Question", "Question_Id", "Definition.Questions");
            DropIndex("Execution.Question", new[] { "Question_Id" });
            DropColumn("Execution.Question", "Question_Id");
        }
    }
}
