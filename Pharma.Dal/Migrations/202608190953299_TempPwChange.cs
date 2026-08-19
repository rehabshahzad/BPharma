namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempPwChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PasswordHash", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            DropColumn("dbo.Employees", "TempPasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "TempPasswordHash", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            DropColumn("dbo.Employees", "PasswordHash");
        }
    }
}
