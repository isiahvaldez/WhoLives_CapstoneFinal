using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _db.Measures.Select(item => new SelectListItem()
            {
                Text = item.MeasureName,
                Value = item.MeasureID.ToString()
            });
        }

        public void update(Measure measures)
        {
            throw new NotImplementedException();
        }
    }
}
