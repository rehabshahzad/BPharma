namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "Address", c => c.String(nullable: false, maxLength: 250, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "Contact", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AlterColumn("dbo.Employees", "Username", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "TempPasswordHash", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            AlterColumn("dbo.Employees", "UpdatedAt", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Employees", "TempPasswordHash", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Username", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Employees", "Contact", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Address", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(unicode: false));
        }
    }
}
