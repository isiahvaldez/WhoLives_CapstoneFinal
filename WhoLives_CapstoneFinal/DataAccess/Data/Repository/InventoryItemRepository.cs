using Microsoft.AspNetCore.Mvc.Rendering;
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
            return _db.InventoryItems.Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.InventoryItemID.ToString()
            });
        }

        public void update(InventoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
