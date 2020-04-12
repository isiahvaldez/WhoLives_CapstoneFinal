using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class BuildAssemblyRepository : Repository<BuildAssembly>, IBuildAssemblyRepository
    {
        private readonly ApplicationDbContext _db;

        public BuildAssemblyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetItemListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void update(BuildAssembly buildAssembly)
        {
            var objFromDb = _db.BuildAssemblies.FirstOrDefault(a => a.BuildAssemblyID == buildAssembly.BuildAssemblyID);
            objFromDb.InventoryItemID = buildAssembly.InventoryItemID;
            objFromDb.AssemblyID = buildAssembly.AssemblyID;
            _db.SaveChanges();
        }
    }
}
