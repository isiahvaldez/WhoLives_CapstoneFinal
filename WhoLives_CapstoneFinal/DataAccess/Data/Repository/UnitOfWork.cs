using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IMeasuresRepository Measures { get; private set; } 
        public IItemRepository Item { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Measures = new MeasuresRepository(_db);
            Item = new ItemRepository(_db);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
