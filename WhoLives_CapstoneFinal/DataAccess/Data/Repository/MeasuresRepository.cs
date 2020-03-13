using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class MeasuresRepository : Repository<Measures>, IMeasuresRepository
    {
        private readonly ApplicationDbContext _db; 
        public MeasuresRepository(ApplicationDbContext db): base(db)
        {
            _db = db; 
        }
        public IEnumerable<SelectListItem> GetMeasuresListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void update(Measures measures)
        {
            throw new NotImplementedException();
        }
    }
}
