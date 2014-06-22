using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ATS.Data.Model;
using System.Data.Entity;

namespace ATS.Data
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ATSCEEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ATSCEEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new ATSCEEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }

        public virtual void Add(params T[] items)
        {
            using (var context = new ATSCEEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new ATSCEEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new ATSCEEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }

        protected static System.Data.EntityState GetEntityState(EntityState entityState)
        {
            switch (entityState)
            {
                case EntityState.Unchanged:
                    return System.Data.EntityState.Unchanged;
                case EntityState.Added:
                    return System.Data.EntityState.Added;
                case EntityState.Modified:
                    return System.Data.EntityState.Modified;
                case EntityState.Deleted:
                    return System.Data.EntityState.Deleted;
                default:
                    return System.Data.EntityState.Detached;
            }
        }
    }
}
