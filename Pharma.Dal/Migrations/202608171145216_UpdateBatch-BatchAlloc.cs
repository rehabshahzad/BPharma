namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBatchBatchAlloc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Batches", "UpdatedAt", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Batches", "UpdatedAt", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
