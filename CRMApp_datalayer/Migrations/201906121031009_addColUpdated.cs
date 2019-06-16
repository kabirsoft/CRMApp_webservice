namespace CRMApp_datalayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Updated", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
            AddColumn("dbo.Customers", "Updated", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
            AddColumn("dbo.Contacts", "Updated", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
            AddColumn("dbo.CustomerTypes", "Updated", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerTypes", "Updated");
            DropColumn("dbo.Contacts", "Updated");
            DropColumn("dbo.Customers", "Updated");
            DropColumn("dbo.Companies", "Updated");
        }
    }
}
