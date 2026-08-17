namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReturnEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReturnItems", "CustomerReturn_CustomerReturnId", c => c.Int());
            AddColumn("dbo.CustomerReturns", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.SupplierReturnItems", "SupplierReturn_SupplierReturnId", c => c.Int());
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Customers", "Contact", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.CustomerReturns", "UpdatedAt", c => c.DateTime(precision: 0));
            AlterColumn("dbo.SupplierReturns", "UpdatedAt", c => c.DateTime(precision: 0));
            CreateIndex("dbo.Customers", "Contact", unique: true);
            CreateIndex("dbo.CustomerReturnItems", "CustomerReturn_CustomerReturnId");
            CreateIndex("dbo.SupplierReturnItems", "SupplierReturn_SupplierReturnId");
            AddForeignKey("dbo.CustomerReturnItems", "CustomerReturn_CustomerReturnId", "dbo.CustomerReturns", "CustomerReturnId");
            AddForeignKey("dbo.SupplierReturnItems", "SupplierReturn_SupplierReturnId", "dbo.SupplierReturns", "SupplierReturnId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierReturnItems", "SupplierReturn_SupplierReturnId", "dbo.SupplierReturns");
            DropForeignKey("dbo.CustomerReturnItems", "CustomerReturn_CustomerReturnId", "dbo.CustomerReturns");
            DropIndex("dbo.SupplierReturnItems", new[] { "SupplierReturn_SupplierReturnId" });
            DropIndex("dbo.CustomerReturnItems", new[] { "CustomerReturn_CustomerReturnId" });
            DropIndex("dbo.Customers", new[] { "Contact" });
            AlterColumn("dbo.SupplierReturns", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.CustomerReturns", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Customers", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Customers", "Contact", c => c.String(unicode: false));
            AlterColumn("dbo.Customers", "LastName", c => c.String(unicode: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(unicode: false));
            DropColumn("dbo.SupplierReturnItems", "SupplierReturn_SupplierReturnId");
            DropColumn("dbo.CustomerReturns", "CreatedAt");
            DropColumn("dbo.CustomerReturnItems", "CustomerReturn_CustomerReturnId");
        }
    }
}
