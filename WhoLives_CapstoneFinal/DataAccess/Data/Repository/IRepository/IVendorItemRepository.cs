using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IVendorItemRepository : IRepository<VendorItem>
    {
        void Update(VendorItem vendorItem);
    }
}
