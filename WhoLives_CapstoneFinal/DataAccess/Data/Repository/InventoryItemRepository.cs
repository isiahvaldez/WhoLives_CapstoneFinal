using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class InventoryItemRepository : Repository<InventoryItem>, IInventoryItemRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetItemListForDropDown()
        {
            return _db.InventoryItems.Where(i => i.IsActive == true).Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.InventoryItemID.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetNonAssemblyItemListForDropDown()
        {
            return _db.InventoryItems.Where(i => i.IsAssembly != true && i.IsActive == true).Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.InventoryItemID.ToString()
            });
        }
        public void Update(InventoryItem item)
        {
            var objFromDb = _db.InventoryItems.FirstOrDefault(v => v.InventoryItemID == item.InventoryItemID);

            objFromDb.IsAssembly = item.IsAssembly;
            objFromDb.LastModifiedBy = item.LastModifiedBy;
            objFromDb.LastModifiedDate = item.LastModifiedDate;
            objFromDb.ListRetailCost = item.ListRetailCost;
            objFromDb.WholeSaleCost = item.WholeSaleCost;
            objFromDb.Measure = item.Measure;
            objFromDb.MeasureAmnt = item.MeasureAmnt;
            objFromDb.MeasuresID = item.MeasuresID;
            objFromDb.Name = item.Name;
            objFromDb.OrderItems = item.OrderItems;
            objFromDb.ReorderQty = item.ReorderQty;
            objFromDb.TotalLooseQty = item.TotalLooseQty;
            objFromDb.VendorItems = item.VendorItems;
            objFromDb.IsActive = item.IsActive;

            _db.SaveChanges();
        }

        public void Delete(InventoryItem item)
        {
            var objFromDb = _db.InventoryItems.FirstOrDefault(v => v.InventoryItemID == item.InventoryItemID);

            objFromDb.IsActive = false;
            _db.SaveChanges();
        }
    }
}
