using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IMeasuresRepository Measures { get; }
        IVendorRepository Vendor { get; }
        IVendorItemRepository VendorItems { get; }
        void Save();
    }
}
