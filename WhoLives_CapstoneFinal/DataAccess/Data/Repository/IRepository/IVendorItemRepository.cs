using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IVendorItemRepository : IRepository<VendorItems>
    {
        void Update(VendorItems vendorItem);
    }
}
