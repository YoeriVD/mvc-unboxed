namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "Title", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surveys", "Title");
        }
    }
}
