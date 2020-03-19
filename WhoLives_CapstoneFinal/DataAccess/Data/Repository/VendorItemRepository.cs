using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class VendorItemRepository : Repository<VendorItem>, IVendorItemRepository
    {
        private readonly ApplicationDbContext _db;

        public VendorItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetVendorListForDropDown()
        {
            return _db.VendorItems.Select(i => new SelectListItem()
            {
                Text = i.Vendor.VendorName,
                Value = i.VendorID.ToString()
            });
        }

        public void Update(VendorItem vendorItem)
        {
            var objFromDb = _db.VendorItems.FirstOrDefault(v => v.VendorID == vendorItem.VendorID);

            objFromDb.VendorID = vendorItem.VendorID;
            objFromDb.InventoryItemID = vendorItem.InventoryItemID;

            _db.SaveChanges();
        }
    }
}
