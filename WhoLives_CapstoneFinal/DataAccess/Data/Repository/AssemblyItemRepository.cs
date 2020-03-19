using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class AssemblyItemRepository : Repository<AssemblyItem>, IAssemblyItemRepository
    {
        private readonly ApplicationDbContext _db;

        public AssemblyItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetItemListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void update(AssemblyItem assemblyItem)
        {
            throw new NotImplementedException();
        }
    }
}
