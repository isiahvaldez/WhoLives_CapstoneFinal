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
        // Logic ref: http://csharpdocs.com/export-data-to-csv-using-c/
        StringBuilder ExportList(IEnumerable<T> list)
        {
            var stringBuilder = new StringBuilder();
            var header = typeof(T).GetProperties();
            for (int i = 0; i < header.Length - 1; i++)
            {
                stringBuilder.Append(header[i].Name + ",");
            }
            var last = header[header.Length - 1].Name;
            stringBuilder.Append(last + Environment.NewLine);
            foreach (var item in list)
            {
                var rowValues = typeof(T).GetProperties();
                for (int i = 0; i < rowValues.Length - 1; i++)
                {
                    var prop = rowValues[i];
                    stringBuilder.Append(prop.GetValue(item) + ",");
                }
                stringBuilder.Append(rowValues[rowValues.Length - 1].GetValue(item) + Environment.NewLine);
            }
            return stringBuilder;
        }

    }
}
