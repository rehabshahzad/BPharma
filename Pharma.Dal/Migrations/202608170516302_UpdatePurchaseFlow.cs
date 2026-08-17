namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePurchaseFlow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "AdditionalCharges", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.Purchases", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseItems", "Status");
            DropColumn("dbo.Purchases", "TaxAmount");
            DropColumn("dbo.Purchases", "DiscountAmount");
            DropColumn("dbo.Purchases", "DeliveryCharges");
            DropColumn("dbo.Purchases", "PurchaseStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "PurchaseStatus", c => c.String(unicode: false));
            AddColumn("dbo.Purchases", "DeliveryCharges", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.Purchases", "DiscountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Purchases", "TaxAmount", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AddColumn("dbo.PurchaseItems", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Purchases", "Status");
            DropColumn("dbo.Purchases", "AdditionalCharges");
        }
    }
}
