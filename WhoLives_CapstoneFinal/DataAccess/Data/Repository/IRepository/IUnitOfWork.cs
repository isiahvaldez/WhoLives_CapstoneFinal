using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAssemblyItemRepository AssemblyItems { get; }
        IInventoryItemRepository InventoryItems { get; }
        IMeasureRepository Measures { get; }
        IPurchaseOrderRepository PurchaseOrders { get; }
        IOrderItemRepository OrderItems { get; }
        IVendorRepository Vendors { get; }
        IVendorItemRepository VendorItems { get; }
        void Save();
    }
}
