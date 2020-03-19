using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class VendorItemRepository : Repository<VendorItems>, IVendorItemRepository
    {
        private readonly ApplicationDbContext _db;

        public VendorItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetVendorListForDropDown()
        {
            return _db.Vendor.Select(i => new SelectListItem()
            {
                Text = i.VendorName,
                Value = i.VendorID.ToString()
            });
        }

        public void Update(VendorItems vendorItem)
        {
            var objFromDb = _db.Vendor.FirstOrDefault(v => v.VendorID == vendorItem.VendorId);

            objFromDb.VendorID = vendorItem.VendorId;
            objFromDb.InventoryItemsId = vendorItem.InventoryItemsId;

            _db.SaveChanges();
        }
    }
}
