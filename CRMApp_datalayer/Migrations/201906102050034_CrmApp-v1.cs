namespace CRMApp_datalayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrmAppv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostAddress = c.String(),
                        Telephone = c.String(),
                        Email = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostAddress = c.String(),
                        Telephone = c.String(),
                        Fax = c.String(),
                        Created = c.DateTime(nullable: false),
                        CompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(),
                        Title = c.String(),
                        Telephone = c.String(),
                        Mobile = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer_CustomerType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        CustomerTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.CustomerTypeId);
            
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer_CustomerType", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Customer_CustomerType", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Customer_CustomerType", new[] { "CustomerTypeId" });
            DropIndex("dbo.Customer_CustomerType", new[] { "CustomerId" });
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.Customer_CustomerType");
            DropTable("dbo.Contacts");
            DropTable("dbo.Customers");
            DropTable("dbo.Companies");
        }
    }
}
