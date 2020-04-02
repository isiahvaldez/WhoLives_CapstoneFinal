using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Get Object 
        T Get(int id);

        // Get All Object 
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = null);

        // Get First of Default 
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        // Add 
        void Add(T entity);
        // Remove by Id
        void Remove(int id);
        // Remove(Object) 
        void Remove(T entity);
        StringBuilder ExportList(IEnumerable<T> list);

    }
}
