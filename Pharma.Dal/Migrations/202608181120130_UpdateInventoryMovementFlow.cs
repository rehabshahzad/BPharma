namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInventoryMovementFlow : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryMovements", "Remarks", c => c.String(maxLength: 500, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryMovements", "Remarks", c => c.String(unicode: false));
        }
    }
}
