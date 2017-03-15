namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyWithQuestionsAndNewSchemas : DbMigration
    {
        public override void Up()
        {
            Sql("truncate table dbo.Surveys");
            RenameTable(name: "dbo.Surveys", newName: "Survey");
            MoveTable(name: "dbo.Survey", newSchema: "Definition");
            CreateTable(
                "Definition.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        QuestionText = c.String(maxLength: 255),
                        ExternalId = c.Guid(nullable: false),
                        Survey_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.Survey", t => t.Survey_Id)
                .Index(t => t.Survey_Id);
            
            CreateTable(
                "Definition.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(maxLength: 255),
                        Score = c.Int(nullable: false),
                        ExternalId = c.Guid(nullable: false),
                        MultipleChoice_Id = c.Int(),
                        MultipleChoice_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.MultipleChoice", t => t.MultipleChoice_Id)
                .ForeignKey("Definition.MultipleChoice", t => t.MultipleChoice_Id1)
                .Index(t => t.MultipleChoice_Id)
                .Index(t => t.MultipleChoice_Id1);
            
            CreateTable(
                "Definition.MultipleChoice",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.Questions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Definition.YesNo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NegativeAnswer_Id = c.Int(),
                        PositiveAnswer_Id = c.Int(),
                        Answer = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.Questions", t => t.Id)
                .ForeignKey("Definition.Answers", t => t.NegativeAnswer_Id)
                .ForeignKey("Definition.Answers", t => t.PositiveAnswer_Id)
                .Index(t => t.Id)
                .Index(t => t.NegativeAnswer_Id)
                .Index(t => t.PositiveAnswer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Definition.YesNo", "PositiveAnswer_Id", "Definition.Answers");
            DropForeignKey("Definition.YesNo", "NegativeAnswer_Id", "Definition.Answers");
            DropForeignKey("Definition.YesNo", "Id", "Definition.Questions");
            DropForeignKey("Definition.MultipleChoice", "Id", "Definition.Questions");
            DropForeignKey("Definition.Questions", "Survey_Id", "Definition.Survey");
            DropForeignKey("Definition.Answers", "MultipleChoice_Id1", "Definition.MultipleChoice");
            DropForeignKey("Definition.Answers", "MultipleChoice_Id", "Definition.MultipleChoice");
            DropIndex("Definition.YesNo", new[] { "PositiveAnswer_Id" });
            DropIndex("Definition.YesNo", new[] { "NegativeAnswer_Id" });
            DropIndex("Definition.YesNo", new[] { "Id" });
            DropIndex("Definition.MultipleChoice", new[] { "Id" });
            DropIndex("Definition.Answers", new[] { "MultipleChoice_Id1" });
            DropIndex("Definition.Answers", new[] { "MultipleChoice_Id" });
            DropIndex("Definition.Questions", new[] { "Survey_Id" });
            DropTable("Definition.YesNo");
            DropTable("Definition.MultipleChoice");
            DropTable("Definition.Answers");
            DropTable("Definition.Questions");
            MoveTable(name: "Definition.Survey", newSchema: "dbo");
            RenameTable(name: "dbo.Survey", newName: "Surveys");
        }
    }
}
