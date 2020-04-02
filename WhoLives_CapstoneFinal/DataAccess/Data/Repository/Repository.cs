using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;

namespace WhoLives.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet; 
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Will be comma seperated 
            if (includeProperties != null)
            {
                foreach (var includeProrerty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProrerty);
                }
            }

            return query.FirstOrDefault();

        }

        public void Remove(int id)
        {
            Remove(dbSet.Find(id));
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        // Logic ref: http://csharpdocs.com/export-data-to-csv-using-c/
        public StringBuilder ExportList(IEnumerable<T> list)
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
