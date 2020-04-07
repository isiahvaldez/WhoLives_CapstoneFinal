using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAssemblyRepository Assemblies { get; }
        IInventoryItemRepository InventoryItems { get; }
        IMeasureRepository Measures { get; }
        IStatusRepository Statuses { get; }
        IPurchaseOrderRepository PurchaseOrders { get; }
        IOrderItemRepository OrderItems { get; }
        IVendorRepository Vendors { get; }
        IVendorItemRepository VendorItems { get; }
        IBuildAssemblyRepository BuildAssemblies { get; }
        void Save();
    }
}
