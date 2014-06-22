using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data
{
    public interface IGenericDataRepository<T> where T : class
    {
        /// <summary>
        /// Get all data
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Get list of data base on specific filter
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Get single object. E.g get data by ID
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Add a new object or insert a record to database
        /// </summary>
        /// <param name="items"></param>
        void Add(params T[] items);

        /// <summary>
        /// Update a specific object or update a record to database
        /// </summary>
        /// <param name="items"></param>
        void Update(params T[] items);

        /// <summary>
        /// Remove a object or delete a record to database
        /// </summary>
        /// <param name="items"></param>
        void Remove(params T[] items);
    }

}
