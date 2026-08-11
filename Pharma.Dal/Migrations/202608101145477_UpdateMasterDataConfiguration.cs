namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMasterDataConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "BrandName", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            AlterColumn("dbo.Formulae", "FormulaName", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Formulae", "UpdatedAt", c => c.DateTime(precision: 0));
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Suppliers", "ContactPersonName", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Suppliers", "ContactNumber", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
            AlterColumn("dbo.Suppliers", "Email", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Suppliers", "Address", c => c.String(nullable: false, maxLength: 250, storeType: "nvarchar"));
            AlterColumn("dbo.Suppliers", "UpdatedAt", c => c.DateTime(precision: 0));
            CreateIndex("dbo.Brands", "BrandName", unique: true);
            CreateIndex("dbo.Formulae", "FormulaName", unique: true);
            CreateIndex("dbo.Suppliers", new[] { "SupplierName", "Email" }, unique: true, name: "IX_Supplier_Name_Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Suppliers", "IX_Supplier_Name_Email");
            DropIndex("dbo.Formulae", new[] { "FormulaName" });
            DropIndex("dbo.Brands", new[] { "BrandName" });
            AlterColumn("dbo.Suppliers", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Suppliers", "Address", c => c.String(unicode: false));
            AlterColumn("dbo.Suppliers", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Suppliers", "ContactNumber", c => c.String(unicode: false));
            AlterColumn("dbo.Suppliers", "ContactPersonName", c => c.String(unicode: false));
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String(unicode: false));
            AlterColumn("dbo.Formulae", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Formulae", "FormulaName", c => c.String(unicode: false));
            AlterColumn("dbo.Categories", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(unicode: false));
            AlterColumn("dbo.Brands", "BrandName", c => c.String(unicode: false));
        }
    }
}
