namespace ArgenGrill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Newsletters", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Newsletters", "Test", c => c.String());
        }
    }
}
