using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class MeasureRepository : Repository<Measure>, IMeasureRepository
    {
        private readonly ApplicationDbContext _db; 
        public MeasureRepository(ApplicationDbContext db): base(db)
        {
            _db = db; 
        }
        public IEnumerable<SelectListItem> GetMeasureListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void update(Measure measures)
        {
            throw new NotImplementedException();
        }
    }
}
