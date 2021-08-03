using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.DAL;
using VehicleService.DAL.Entity;
using VehicleService.Model;
using VehicleService.Model.Common;
using VehicleService.Repository.Common;

namespace VehicleService.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        protected VehicleServiceContext _DbContext;
        private readonly IMapper mapper;
        public VehicleMakeRepository(VehicleServiceContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<IVehicleMake> GetByIdAsync(int id)
        {
            var entity = await _DbContext.Set<VehicleMakeEntity>().FindAsync(id);
            var make = mapper.Map<IVehicleMake>(entity);
            return make;
        }

        public async Task<IEnumerable<IVehicleMake>> GetAllAsync()
        {
            var entityList = await _DbContext.VehicleMakes.ToListAsync();
            var make = mapper.Map<IEnumerable<IVehicleMake>>(entityList);
            return make;
        }

        public async Task<IPagedList<IVehicleMake>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page)
        {
            Func<IQueryable<VehicleMakeEntity>, IOrderedQueryable<VehicleMakeEntity>> orderBy = null;

            switch (sort.SortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(s => s.Name);
                    break;
                case "arbv_desc":
                    orderBy = q => q.OrderByDescending(s => s.Abrv);
                    break;
                case "arbv":
                    orderBy = q => q.OrderBy(s => s.Abrv);
                    break;
                default:
                    orderBy = q => q.OrderBy(s => s.Name);
                    break;
            }

            var entityList = await orderBy(_DbContext.VehicleMakes).Where(x => x.Name.Contains(filterName.SearchName) || filterName.SearchName == null).ToListAsync();
            var makeList = mapper.Map<IEnumerable<IVehicleMake>>(entityList);
            return makeList.ToPagedList(page.CurrentPage, page.ItemsPerPage);

        }

        public async Task<int> AddAsync(IVehicleMake vehicleMakeAdd)
        {
            try
            {
                var entity = mapper.Map<VehicleMakeEntity>(vehicleMakeAdd);

                DbEntityEntry dbEntityEntry = _DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    _DbContext.Set<VehicleMakeEntity>().Add(entity);
                }
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(IVehicleMake vehicleMakeUpdate)
        {
            var entity = mapper.Map<VehicleMakeEntity>(vehicleMakeUpdate);
            try
            {
                DbEntityEntry dbEntityEntry = _DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    _DbContext.Set<VehicleMakeEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Task<int> DeleteAsync(int id)
        {
            var entity = _DbContext.Set<VehicleMakeEntity>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            try
            {
                DbEntityEntry dbEntityEntry = _DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _DbContext.Set<VehicleMakeEntity>().Attach(entity);
                    _DbContext.Set<VehicleMakeEntity>().Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;

            }
        }

    }
}
