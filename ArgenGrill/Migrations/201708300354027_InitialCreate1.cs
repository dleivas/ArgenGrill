namespace ArgenGrill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Newsletters", "Test");
            DropColumn("dbo.Newsletters", "Test2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Newsletters", "Test2", c => c.String());
            AddColumn("dbo.Newsletters", "Test", c => c.String());
        }
    }
}
