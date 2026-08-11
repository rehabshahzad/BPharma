using Pharma.Entity.Entities;
using PharmacyManagement.Entity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Pharma.DAL.Context
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext() //constructor
            : base("name=PharmacyDbConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SupplierItem> SupplierItems { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Batch> Batches { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<BatchAllocation> BatchAllocations { get; set; }

        public DbSet<InventoryMovement> InventoryMovements { get; set; }

        public DbSet<CustomerReturn> CustomerReturns { get; set; }
        public DbSet<CustomerReturnItem> CustomerReturnItems { get; set; }

        public DbSet<SupplierReturn> SupplierReturns { get; set; }
        public DbSet<SupplierReturnItem> SupplierReturnItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //OnModelCreating() defines how tables are related
        {

            //EMPLOYEE
            modelBuilder.Entity<Employee>()
    .Property(e => e.FirstName)
    .IsRequired()
    .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Contact)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TempPasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.CreatedByEmployee)// may or may not have a creator (admin)
                .WithMany() //one emp can create many emp
                .HasForeignKey(e => e.CreatedByEmployeeId)
                .WillCascadeOnDelete(false); //uss emp k delete hone k sath uske created emp delete nahi honge

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.UpdatedByEmployee)
                .WithMany() //one emp can update many emp
                .HasForeignKey(e => e.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //CUSTOMER


            modelBuilder.Entity<Customer>()
                    .HasRequired(c => c.CreatedByEmployee)
                    .WithMany() //one emp can create many customers
                    .HasForeignKey(c => c.CreatedByEmployeeId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasOptional(c=> c.UpdatedByEmployee) //has optional and has required -> expects a navigation property
                .WithMany()
                .HasForeignKey(c=> c.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //SUPPLIER
            modelBuilder.Entity<Supplier>()
                .Property(s => s.SupplierName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute(
                            "IX_Supplier_Name_Email",
                            1
                        )
                        {
                            IsUnique = true
                        }
                    )
                );

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute(
                            "IX_Supplier_Name_Email",
                            2
                        )
                        {
                            IsUnique = true
                        }
                    )
                );

            modelBuilder.Entity<Supplier>()
                .Property(s => s.ContactPersonName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Supplier>()
                .Property(s => s.ContactNumber)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Supplier>()
                .HasRequired(s => s.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasOptional(s => s.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //CATEGORY

            modelBuilder.Entity<Category>()
               .Property(c => c.CategoryName)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Category>()
                .HasRequired(c => c.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(c => c.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasOptional(c => c.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(c => c.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //BRAND

            modelBuilder.Entity<Brand>()
                .Property(b => b.BrandName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,

                new IndexAnnotation(
                    new IndexAttribute("IX_BrandName") //ef6 core accepts IsUnique()
                    {
                        IsUnique = true
                    }
                    )

                );

            modelBuilder.Entity<Brand>()
                .HasRequired(s => s.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                 .HasOptional(s => s.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //FORMULA

            modelBuilder.Entity<Formula>()
                .HasRequired(s => s.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Formula>()
               .HasOptional(s => s.UpdatedByEmployee)
               .WithMany()
               .HasForeignKey(s => s.UpdatedByEmployeeId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Formula>()
                .Property(f => f.FormulaName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_FormulaName")
                        {
                            IsUnique = true
                        }
                    )
                );

            //ITEM

            modelBuilder.Entity<Item>()
                 .HasRequired(s => s.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
               .HasOptional(s => s.UpdatedByEmployee)
               .WithMany()
               .HasForeignKey(s => s.UpdatedByEmployeeId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasRequired( i=> i.Category)
                .WithMany() //aik category k kafi zada item ho skte haen
                .HasForeignKey(i=> i.CategoryId)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Item>()
                .HasRequired(i=> i.Brand)
                .WithMany() //one brand has many items
                .HasForeignKey(i=> i.BrandId)
                .WillCascadeOnDelete(false); //if brand, category or formula dels we still need the historical records return and other cases 

            modelBuilder.Entity<Item>()
                 .HasOptional(i => i.Formula) //not all items have formula
                .WithMany() 
                .HasForeignKey(i => i.FormulaId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Item>()
            .Property(i => i.SellingPrice)
            .HasPrecision(12, 2);

            //SUPPLIER ITEM

            modelBuilder.Entity<SupplierItem>()
         .HasRequired(si => si.Supplier)
         .WithMany()
         .HasForeignKey(si => si.SupplierId)
         .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierItem>()
                .HasRequired(si => si.Item)
                .WithMany()
                .HasForeignKey(si => si.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierItem>()
                .Property(si => si.SupplierPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(pi => pi.UnitPurchasePrice)
                .HasPrecision(12, 2);



            modelBuilder.Entity<SupplierItem>()
           .HasRequired(si => si.CreatedByEmployee)
           .WithMany()
           .HasForeignKey(si => si.CreatedByEmployeeId)
           .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierItem>()
                .HasOptional(si => si.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(si => si.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);


            //PURCHASE

            modelBuilder.Entity<Purchase>()
               .HasRequired(p => p.Supplier)
               .WithMany()
               .HasForeignKey(p => p.SupplierId)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<Purchase>()
              .HasRequired(p => p.CreatedByEmployee)
              .WithMany()
              .HasForeignKey(p => p.CreatedByEmployeeId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase>()
                .HasOptional(p => p.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(p => p.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.SubtotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.TaxAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.DeliveryCharges)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.TotalAmount)
                .HasPrecision(12, 2);


            //PURCHASE ITEM
            modelBuilder.Entity<PurchaseItem>()
                .HasRequired(pi => pi.Purchase)
                .WithMany()
                .HasForeignKey(pi => pi.PurchaseId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<PurchaseItem>()
                .HasRequired(pi => pi.Item)
                .WithMany()
                .HasForeignKey(pi => pi.ItemId)
                .WillCascadeOnDelete(false);


            //BATCH

            modelBuilder.Entity<Batch>()
              .HasRequired(b => b.PurchaseItem)
              .WithMany()
              .HasForeignKey(b => b.PurchaseItemId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Batch>()
                .HasRequired(b => b.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(b => b.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Batch>()
            .HasOptional(b => b.UpdatedByEmployee)
            .WithMany()
            .HasForeignKey(b => b.UpdatedByEmployeeId)
            .WillCascadeOnDelete(false);


            //SALE
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.SoldByEmployee)
                .WithMany()
                .HasForeignKey(s => s.SoldByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasOptional(s => s.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .Property(s => s.SubtotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.DiscountAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasPrecision(12, 2);

            //SALE ITEM

            modelBuilder.Entity<SaleItem>()
                .HasRequired(si => si.Sale)
                .WithMany()
                .HasForeignKey(si => si.SaleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SaleItem>()
                .HasRequired(si => si.Item)
                .WithMany()
                .HasForeignKey(si => si.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.UnitPrice)
                .HasPrecision(12, 2);

            //BATCH ALLOCATION
            modelBuilder.Entity<BatchAllocation>()
                .HasRequired(ba => ba.SaleItem)
                .WithMany()
                .HasForeignKey(ba => ba.SaleItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BatchAllocation>()
                .HasRequired(ba => ba.Batch)
                .WithMany()
                .HasForeignKey(ba => ba.BatchId)
                .WillCascadeOnDelete(false);

            //INVENTORY MOVEMENT
            modelBuilder.Entity<InventoryMovement>()
                .HasRequired(im => im.Batch)
                .WithMany()
                .HasForeignKey(im => im.BatchId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InventoryMovement>()
                .HasRequired(im => im.PerformedByEmployee)
                .WithMany()
                .HasForeignKey(im => im.PerformedByEmployeeId)
                .WillCascadeOnDelete(false);
        

            //SUPPLIER RETURN
            modelBuilder.Entity<SupplierReturn>()
               .HasRequired(sr => sr.Purchase)
               .WithMany()
               .HasForeignKey(sr => sr.PurchaseId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierReturn>()
                .HasRequired(sr => sr.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(sr => sr.CreatedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierReturn>()
                .HasOptional(sr => sr.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(sr => sr.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //SUPPLIER RETURN ITEM
            modelBuilder.Entity<SupplierReturnItem>()
                .HasRequired(sri => sri.SupplierReturn)
                .WithMany()
                .HasForeignKey(sri => sri.SupplierReturnId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierReturnItem>()
                .HasRequired(sri => sri.Batch)
                .WithMany()
                .HasForeignKey(sri => sri.BatchId)
                .WillCascadeOnDelete(false);

            //CUSTOMER RETURN
            modelBuilder.Entity<CustomerReturn>()
                .HasRequired(cr => cr.Sale)
                .WithMany()
                .HasForeignKey(cr => cr.SaleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerReturn>()
                .HasRequired(cr => cr.ReceivedByEmployee)
                .WithMany()
                .HasForeignKey(cr => cr.ReceivedByEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerReturn>()
                .HasOptional(cr => cr.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(cr => cr.UpdatedByEmployeeId)
                .WillCascadeOnDelete(false);

            //CUSTOMER RETURN ITEM

            modelBuilder.Entity<CustomerReturnItem>()
                .HasRequired(cri => cri.CustomerReturn)
                .WithMany()
                .HasForeignKey(cri => cri.CustomerReturnId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerReturnItem>()
                .HasRequired(cri => cri.SaleItem)
                .WithMany()
                .HasForeignKey(cri => cri.SaleItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerReturnItem>()
                .HasRequired(cri => cri.Batch)
                .WithMany()
                .HasForeignKey(cri => cri.BatchId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }


    }
}