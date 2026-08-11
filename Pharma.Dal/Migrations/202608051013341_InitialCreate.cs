namespace Pharma.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BatchAllocations",
                c => new
                    {
                        BatchAllocationId = c.Int(nullable: false, identity: true),
                        SaleItemId = c.Int(nullable: false),
                        BatchId = c.Int(nullable: false),
                        AllocatedQuantity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.BatchAllocationId)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .ForeignKey("dbo.SaleItems", t => t.SaleItemId)
                .Index(t => t.SaleItemId)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        PurchaseItemId = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        BatchNumber = c.String(unicode: false),
                        ReceivedQuantity = c.Int(nullable: false),
                        ManufacturingDate = c.DateTime(precision: 0),
                        ExpiryDate = c.DateTime(nullable: false, precision: 0),
                        ReceivedDate = c.DateTime(nullable: false, precision: 0),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.BatchId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.PurchaseItems", t => t.PurchaseItemId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.PurchaseItemId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Contact = c.String(unicode: false),
                        Role = c.Int(nullable: false),
                        startDate = c.DateTime(nullable: false, precision: 0),
                        endDate = c.DateTime(precision: 0),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isActive = c.Boolean(nullable: false),
                        Username = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        tempPasswordHash = c.String(unicode: false),
                        isPasswordChanged = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        PurchaseItemId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        OrderedQuantity = c.Int(nullable: false),
                        UnitPurchasePrice = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseItemId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .Index(t => t.PurchaseId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        FormulaId = c.Int(),
                        ItemName = c.String(unicode: false),
                        ItemType = c.Int(nullable: false),
                        Description = c.String(unicode: false),
                        PictureUrl = c.String(unicode: false),
                        Barcode = c.String(unicode: false),
                        IsPrescriptionRequired = c.Boolean(nullable: false),
                        SellingPrice = c.Decimal(nullable: false, precision: 12, scale: 2),
                        MinimumStockLevel = c.Int(nullable: false),
                        RackNumber = c.String(unicode: false),
                        ShelfNumber = c.String(unicode: false),
                        LaneNumber = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Formulae", t => t.FormulaId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.FormulaId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.BrandId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Formulae",
                c => new
                    {
                        FormulaId = c.Int(nullable: false, identity: true),
                        FormulaName = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        isActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.FormulaId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false, precision: 0),
                        SubtotalAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveryCharges = c.Decimal(nullable: false, precision: 12, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Notes = c.String(unicode: false),
                        PurchaseStatus = c.String(unicode: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.SupplierId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(unicode: false),
                        ContactPersonName = c.String(unicode: false),
                        ContactNumber = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.SupplierId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.SaleItems",
                c => new
                    {
                        SaleItemId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 12, scale: 2),
                    })
                .PrimaryKey(t => t.SaleItemId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false, precision: 0),
                        SubtotalAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Status = c.Int(nullable: false),
                        SoldByEmployeeId = c.Int(nullable: false),
                        Notes = c.String(unicode: false),
                        SoldAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Employees", t => t.SoldByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CustomerId)
                .Index(t => t.SoldByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Contact = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.CustomerReturnItems",
                c => new
                    {
                        CustomerReturnItemId = c.Int(nullable: false, identity: true),
                        CustomerReturnId = c.Int(nullable: false),
                        SaleItemId = c.Int(nullable: false),
                        BatchId = c.Int(nullable: false),
                        ReturnQuantity = c.Int(nullable: false),
                        RefundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reason = c.String(unicode: false),
                        CanReturnToStock = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerReturnItemId)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .ForeignKey("dbo.CustomerReturns", t => t.CustomerReturnId)
                .ForeignKey("dbo.SaleItems", t => t.SaleItemId)
                .Index(t => t.CustomerReturnId)
                .Index(t => t.SaleItemId)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.CustomerReturns",
                c => new
                    {
                        CustomerReturnId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false, precision: 0),
                        Remarks = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        ReceivedByEmployeeId = c.Int(nullable: false),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.CustomerReturnId)
                .ForeignKey("dbo.Employees", t => t.ReceivedByEmployeeId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.SaleId)
                .Index(t => t.ReceivedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.InventoryMovements",
                c => new
                    {
                        InventoryMovementId = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        MovementType = c.Int(nullable: false),
                        QuantityChange = c.Int(nullable: false),
                        ReferenceId = c.Int(),
                        Remarks = c.String(unicode: false),
                        MovementDate = c.DateTime(nullable: false, precision: 0),
                        PerformedByEmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryMovementId)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .ForeignKey("dbo.Employees", t => t.PerformedByEmployeeId)
                .Index(t => t.BatchId)
                .Index(t => t.PerformedByEmployeeId);
            
            CreateTable(
                "dbo.SupplierItems",
                c => new
                    {
                        SupplierItemId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        SupplierPrice = c.Decimal(nullable: false, precision: 12, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.SupplierItemId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.SupplierId)
                .Index(t => t.ItemId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
            CreateTable(
                "dbo.SupplierReturnItems",
                c => new
                    {
                        SupplierReturnItemId = c.Int(nullable: false, identity: true),
                        SupplierReturnId = c.Int(nullable: false),
                        BatchId = c.Int(nullable: false),
                        ReturnQuantity = c.Int(nullable: false),
                        ReturnAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reason = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.SupplierReturnItemId)
                .ForeignKey("dbo.Batches", t => t.BatchId)
                .ForeignKey("dbo.SupplierReturns", t => t.SupplierReturnId)
                .Index(t => t.SupplierReturnId)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.SupplierReturns",
                c => new
                    {
                        SupplierReturnId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false, precision: 0),
                        Reason = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        CreatedByEmployeeId = c.Int(nullable: false),
                        UpdatedByEmployeeId = c.Int(),
                        UpdatedAt = c.DateTime(nullable: false, precision: 0),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.SupplierReturnId)
                .ForeignKey("dbo.Employees", t => t.CreatedByEmployeeId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.Employees", t => t.UpdatedByEmployeeId)
                .Index(t => t.PurchaseId)
                .Index(t => t.CreatedByEmployeeId)
                .Index(t => t.UpdatedByEmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierReturnItems", "SupplierReturnId", "dbo.SupplierReturns");
            DropForeignKey("dbo.SupplierReturns", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.SupplierReturns", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.SupplierReturns", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.SupplierReturnItems", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.SupplierItems", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.SupplierItems", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.SupplierItems", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.InventoryMovements", "PerformedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.InventoryMovements", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.CustomerReturnItems", "SaleItemId", "dbo.SaleItems");
            DropForeignKey("dbo.CustomerReturnItems", "CustomerReturnId", "dbo.CustomerReturns");
            DropForeignKey("dbo.CustomerReturns", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CustomerReturns", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.CustomerReturns", "ReceivedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CustomerReturnItems", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.BatchAllocations", "SaleItemId", "dbo.SaleItems");
            DropForeignKey("dbo.SaleItems", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Sales", "SoldByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Customers", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.SaleItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BatchAllocations", "BatchId", "dbo.Batches");
            DropForeignKey("dbo.Batches", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Batches", "PurchaseItemId", "dbo.PurchaseItems");
            DropForeignKey("dbo.PurchaseItems", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Suppliers", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Purchases", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PurchaseItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "FormulaId", "dbo.Formulae");
            DropForeignKey("dbo.Formulae", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Formulae", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Categories", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Brands", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Batches", "CreatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "UpdatedByEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CreatedByEmployeeId", "dbo.Employees");
            DropIndex("dbo.SupplierReturns", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.SupplierReturns", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.SupplierReturns", new[] { "PurchaseId" });
            DropIndex("dbo.SupplierReturnItems", new[] { "BatchId" });
            DropIndex("dbo.SupplierReturnItems", new[] { "SupplierReturnId" });
            DropIndex("dbo.SupplierItems", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.SupplierItems", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.SupplierItems", new[] { "ItemId" });
            DropIndex("dbo.SupplierItems", new[] { "SupplierId" });
            DropIndex("dbo.InventoryMovements", new[] { "PerformedByEmployeeId" });
            DropIndex("dbo.InventoryMovements", new[] { "BatchId" });
            DropIndex("dbo.CustomerReturns", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.CustomerReturns", new[] { "ReceivedByEmployeeId" });
            DropIndex("dbo.CustomerReturns", new[] { "SaleId" });
            DropIndex("dbo.CustomerReturnItems", new[] { "BatchId" });
            DropIndex("dbo.CustomerReturnItems", new[] { "SaleItemId" });
            DropIndex("dbo.CustomerReturnItems", new[] { "CustomerReturnId" });
            DropIndex("dbo.Customers", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Customers", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Sales", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Sales", new[] { "SoldByEmployeeId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleItems", new[] { "ItemId" });
            DropIndex("dbo.SaleItems", new[] { "SaleId" });
            DropIndex("dbo.Suppliers", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Suppliers", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Purchases", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Purchases", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.Formulae", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Formulae", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Categories", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Categories", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Brands", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Brands", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Items", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Items", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Items", new[] { "FormulaId" });
            DropIndex("dbo.Items", new[] { "BrandId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.PurchaseItems", new[] { "ItemId" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseId" });
            DropIndex("dbo.Employees", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Employees", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Batches", new[] { "UpdatedByEmployeeId" });
            DropIndex("dbo.Batches", new[] { "CreatedByEmployeeId" });
            DropIndex("dbo.Batches", new[] { "PurchaseItemId" });
            DropIndex("dbo.BatchAllocations", new[] { "BatchId" });
            DropIndex("dbo.BatchAllocations", new[] { "SaleItemId" });
            DropTable("dbo.SupplierReturns");
            DropTable("dbo.SupplierReturnItems");
            DropTable("dbo.SupplierItems");
            DropTable("dbo.InventoryMovements");
            DropTable("dbo.CustomerReturns");
            DropTable("dbo.CustomerReturnItems");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleItems");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.Formulae");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Items");
            DropTable("dbo.PurchaseItems");
            DropTable("dbo.Employees");
            DropTable("dbo.Batches");
            DropTable("dbo.BatchAllocations");
        }
    }
}
