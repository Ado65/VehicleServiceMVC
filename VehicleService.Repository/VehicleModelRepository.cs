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
using VehicleService.Model.Common;
using VehicleService.Repository.Common;

namespace VehicleService.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        protected VehicleServiceContext _DbContext;
        private readonly IMapper _mapper;
        public VehicleModelRepository(VehicleServiceContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        public async Task<IVehicleModel> GetByIdAsync(int id)
        {
            var entity = await _DbContext.Set<VehicleModelEntity>().FindAsync(id);
            var model = _mapper.Map<IVehicleModel>(entity);
            return model;
        }


        public async Task<IEnumerable<IVehicleModel>> GetAllAsync()
        {
            var entityList = await _DbContext.Set<VehicleModelEntity>().ToListAsync();
            var modelList = _mapper.Map<IEnumerable<IVehicleModel>>(entityList);
            return modelList;
        }

        public async Task<IPagedList<IVehicleModel>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page)
        {
            Func<IQueryable<VehicleModelEntity>, IOrderedQueryable<VehicleModelEntity>> orderBy = null;

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

            IEnumerable<VehicleModelEntity> entityList = await orderBy(_DbContext.Set<VehicleModelEntity>()).Where(x => x.Name.Contains(filterName.SearchName) || filterName.SearchName == null).ToListAsync();
            var makeList = _mapper.Map<IEnumerable<IVehicleModel>>(entityList);
            return makeList.ToPagedList(page.CurrentPage, page.ItemsPerPage);
        }

        public async Task<int> AddAsync(IVehicleModel vehicleModleAdd)
        {
            try
            {
                var entity = _mapper.Map<VehicleModelEntity>(vehicleModleAdd);

                DbEntityEntry dbEntityEntry = _DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    _DbContext.Set<VehicleModelEntity>().Add(entity);
                }
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateAsync(IVehicleModel vehicleModelUpdate)
        {
            var entity = _mapper.Map<VehicleModelEntity>(vehicleModelUpdate);
            try
            {
                DbEntityEntry dbEntityEntry = _DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    _DbContext.Set<VehicleModelEntity>().Attach(entity);
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
            var entity = _DbContext.Set<VehicleModelEntity>().Find(id);
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
                    _DbContext.Set<VehicleModelEntity>().Attach(entity);
                    _DbContext.Set<VehicleModelEntity>().Remove(entity);
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
