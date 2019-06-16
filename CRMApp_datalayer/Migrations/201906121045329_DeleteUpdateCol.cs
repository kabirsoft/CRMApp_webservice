namespace CRMApp_datalayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUpdateCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "Updated");
            DropColumn("dbo.Customers", "Updated");
            DropColumn("dbo.Contacts", "Updated");
            DropColumn("dbo.CustomerTypes", "Updated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerTypes", "Updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "Updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "Updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "Updated", c => c.DateTime(nullable: false));
        }
    }
}
