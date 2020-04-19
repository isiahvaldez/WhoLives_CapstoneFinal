using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        IEnumerable<Vendor> GetAllActive(Expression<Func<Vendor, bool>> filter = null, Func<IQueryable<Vendor>, IOrderedQueryable<Vendor>> orderby = null, string includeProperties = null);
        IEnumerable<SelectListItem> GetVendorListForDropDown();
        void Update(Vendor vendor);
    }
}
