using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAssemblyItemRepository AssemblyItems { get; private set; }
        public IMeasureRepository Measures { get; private set; } 
        public IInventoryItemRepository InventoryItems { get; private set; }

        public IPurchaseOrderRepository PurchaseOrders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public IVendorRepository Vendors { get; private set; }
        public IVendorItemRepository VendorItems { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AssemblyItems = new AssemblyItemRepository(_db);
            Measures = new MeasureRepository(_db);
            InventoryItems = new InventoryItemRepository(_db);
            Measures = new MeasureRepository(_db);
            PurchaseOrders = new PurchaseOrderRepository(_db);
            OrderItems = new OrderItemRepository(_db);
            Vendors = new VendorRepository(_db);
            VendorItems = new VendorItemRepository(_db);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
