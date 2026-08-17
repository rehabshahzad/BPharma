namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSaleFlow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleItems", "OrderedQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.SaleItems", "UnitSalePrice", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.Sales", "AdditionalCharges", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            DropColumn("dbo.SaleItems", "Quantity");
            DropColumn("dbo.SaleItems", "UnitPrice");
            DropColumn("dbo.Sales", "DiscountAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "DiscountAmount", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.SaleItems", "UnitPrice", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.SaleItems", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Sales", "AdditionalCharges");
            DropColumn("dbo.SaleItems", "UnitSalePrice");
            DropColumn("dbo.SaleItems", "OrderedQuantity");
        }
    }
}
