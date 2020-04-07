﻿using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAssemblyRepository Assemblies { get; private set; }
        public IMeasureRepository Measures { get; private set; } 
        public IStatusRepository Statuses { get; private set; }
        public IInventoryItemRepository InventoryItems { get; private set; }
        public IPurchaseOrderRepository PurchaseOrders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public IVendorRepository Vendors { get; private set; }
        public IVendorItemRepository VendorItems { get; private set; }
        public IBuildAssemblyRepository BuildAssemblies { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Assemblies = new AssemblyRepository(_db);
            Measures = new MeasureRepository(_db);
            Statuses = new StatusRepository(_db);
            InventoryItems = new InventoryItemRepository(_db);
            Measures = new MeasureRepository(_db);
            PurchaseOrders = new PurchaseOrderRepository(_db);
            OrderItems = new OrderItemRepository(_db);
            Vendors = new VendorRepository(_db);
            VendorItems = new VendorItemRepository(_db);
            BuildAssemblies = new BuildAssemblyRepository(_db);
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
