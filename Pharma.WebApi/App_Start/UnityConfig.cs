using Pharma.BLL.Security;
using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.DAL.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Pharma.WebApi.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<PharmacyDbContext>(
                new HierarchicalLifetimeManager()
            );

            //Employee / auth
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<AuthService>();

            //Brand
            container.RegisterType<IBrandRepository, BrandRepository>();
            container.RegisterType<IBrandService, BrandService>();

            //Batch
            container.RegisterType<IBatchRepository, BatchRepository>();
            container.RegisterType<IBatchService, BatchService>();

            //Category
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ICategoryService, CategoryService>();

            //Customer
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<ICustomerService, CustomerService>();

            //Customer Return
            container.RegisterType<ICustomerReturnRepository, CustomerReturnRepository>();
            container.RegisterType<ICustomerReturnService, CustomerReturnService>();

            //Formula
            container.RegisterType<IFormulaRepository, FormulaRepository>();
            container.RegisterType<IFormulaService, FormulaService>();

            //InventoryMovements
            container.RegisterType<IInventoryMovementRepository, InventoryMovementRepository>();
            container.RegisterType<IInventoryMovementService, InventoryMovementService>();


            // Item
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IItemService, ItemService>();

            // Supplier
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<ISupplierService, SupplierService>();

            // Supplier Return
            container.RegisterType<ISupplierReturnRepository, SupplierReturnRepository>();
            container.RegisterType<ISupplierReturnService, SupplierReturnService>();

            // Purchase
            container.RegisterType<IPurchaseRepository, PurchaseRepository>();
            container.RegisterType<IPurchaseService, PurchaseService>();

            // Supplier Item
            container.RegisterType<ISupplierItemRepository, SupplierItemRepository>();
            container.RegisterType<ISupplierItemService, SupplierItemService>();

            // Sale
            container.RegisterType<ISaleRepository, SaleRepository>();
            container.RegisterType<ISaleService, SaleService>();

            //Telling Web API to use Unity for DI
            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}