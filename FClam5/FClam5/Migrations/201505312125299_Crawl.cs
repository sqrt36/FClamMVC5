namespace FClam5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Crawl : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ErrorReports");
            AlterColumn("dbo.ErrorReports", "errorNumber", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ErrorReports", new[] { "reportNumber", "errorNumber" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ErrorReports");
            AlterColumn("dbo.ErrorReports", "errorNumber", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ErrorReports", "errorNumber");
        }
    }
}
