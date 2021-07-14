using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleService.DAL;
using VehicleService.Repository.Common;

namespace VehicleService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected VehicleServiceContext _DbContext { get; private set; }
        public GenericRepository(VehicleServiceContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _DbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            IQueryable<T> query = _DbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();

            }
            else
            {
                return await query.ToListAsync();

            }
        }
    }
}
