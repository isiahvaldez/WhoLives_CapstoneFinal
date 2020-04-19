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
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly ApplicationDbContext _db;

        public VendorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetVendorListForDropDown()
        {
            return _db.Vendors.Select(i => new SelectListItem()
            {
                Text = i.VendorName,
                Value = i.VendorID.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetActiveVendorListForDropDown()
        {
            return _db.Vendors.Where(i => i.isActive == true).Select(i => new SelectListItem()
            {
                Text = i.VendorName,
                Value = i.VendorID.ToString()
            });
        }

        public void Update(Vendor vendor)
        {
            var objFromDb = _db.Vendors.FirstOrDefault(v => v.VendorID == vendor.VendorID);

            objFromDb.VendorName = vendor.VendorName;
            objFromDb.VendorWebsite = vendor.VendorWebsite;
            objFromDb.PhoneNumber = vendor.PhoneNumber;
            vendor.LastModifiedDate = DateTime.Now;
            objFromDb.LastModifiedDate = DateTime.Now;
            objFromDb.isActive = vendor.isActive;

            _db.SaveChanges();
        }
    }
}
