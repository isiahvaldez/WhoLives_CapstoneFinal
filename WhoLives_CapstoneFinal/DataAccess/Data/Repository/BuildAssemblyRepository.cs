using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
