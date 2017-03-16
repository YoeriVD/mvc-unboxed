namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValuesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("Execution.Question", "Answer", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("Execution.Question", "Answer");
        }
    }
}
