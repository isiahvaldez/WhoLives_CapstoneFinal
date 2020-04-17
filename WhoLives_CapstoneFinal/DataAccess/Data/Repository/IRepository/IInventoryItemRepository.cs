using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IInventoryItemRepository : IRepository<InventoryItem>
    {
        IEnumerable<SelectListItem> GetItemListForDropDown();

        IEnumerable<SelectListItem> GetNonAssemblyItemListForDropDown();
        void Update(InventoryItem item);

        public void Delete(InventoryItem item);
    }
}
