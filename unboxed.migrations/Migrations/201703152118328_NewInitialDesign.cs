namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInitialDesign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Execution.Survey",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        BasedOn_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.Survey", t => t.BasedOn_Id)
                .Index(t => t.BasedOn_Id);
            
            CreateTable(
                "Definition.Survey",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        ExternalId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Definition.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Definition.MultipleChoice", t => t.MultipleChoice_Id)
                .Index(t => t.MultipleChoice_Id);
            
            CreateTable(
                "Execution.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        ExternalId = c.Guid(nullable: false),
                        SurveyInstance_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Execution.Survey", t => t.SurveyInstance_Id)
                .Index(t => t.SurveyInstance_Id);
            
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
            DropForeignKey("Execution.Question", "SurveyInstance_Id", "Execution.Survey");
            DropForeignKey("Execution.Survey", "BasedOn_Id", "Definition.Survey");
            DropForeignKey("Definition.Questions", "Survey_Id", "Definition.Survey");
            DropForeignKey("Definition.Answers", "MultipleChoice_Id", "Definition.MultipleChoice");
            DropIndex("Definition.YesNo", new[] { "PositiveAnswer_Id" });
            DropIndex("Definition.YesNo", new[] { "NegativeAnswer_Id" });
            DropIndex("Definition.YesNo", new[] { "Id" });
            DropIndex("Definition.MultipleChoice", new[] { "Id" });
            DropIndex("Execution.Question", new[] { "SurveyInstance_Id" });
            DropIndex("Definition.Answers", new[] { "MultipleChoice_Id" });
            DropIndex("Definition.Questions", new[] { "Survey_Id" });
            DropIndex("Execution.Survey", new[] { "BasedOn_Id" });
            DropTable("Definition.YesNo");
            DropTable("Definition.MultipleChoice");
            DropTable("Execution.Question");
            DropTable("Definition.Answers");
            DropTable("Definition.Questions");
            DropTable("Definition.Survey");
            DropTable("Execution.Survey");
        }
    }
}
