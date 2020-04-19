using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IInventoryItemRepository : IRepository<InventoryItem>
    {
        public IEnumerable<InventoryItem> GetAllActive(Expression<Func<InventoryItem, bool>> filter = null, Func<IQueryable<InventoryItem>, IOrderedQueryable<InventoryItem>> orderby = null, string includeProperties = null);
        IEnumerable<SelectListItem> GetItemListForDropDown();

        IEnumerable<SelectListItem> GetNonAssemblyItemListForDropDown();

        void Update(InventoryItem item);

        public void Delete(InventoryItem item);
    }
}
