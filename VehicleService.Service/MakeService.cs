using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleService.DAL.Entity;
using VehicleService.Model.Common;
using VehicleService.Repository.Common;
using VehicleService.Service.Common;

namespace VehicleService.Service
{
    public class MakeService : IMakeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<VehicleMake> genericRepository;
        private readonly IMapper mapper;

        public MakeService(IUnitOfWork unitOfWork, IGenericRepository<VehicleMake> genericRepository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }
        public async Task AddAsync(IViewModelVehicleMake makeToAdd)
        {
            await unitOfWork.AddAsync<VehicleMake>(mapper.Map<IViewModelVehicleMake, VehicleMake>(makeToAdd));
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(IViewModelVehicleMake makeToUpdate)
        {
            await unitOfWork.UpdateAsync<VehicleMake>(mapper.Map<IViewModelVehicleMake, VehicleMake>(makeToUpdate));
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.DeleteAsync<VehicleMake>(id);
            await unitOfWork.CommitAsync();
        }

        public async Task<IViewModelVehicleMake> GetByIdAsync(int id)
        {
            var makeToGet = await genericRepository.GetByIdAsync(id);
            return mapper.Map<VehicleMake, IViewModelVehicleMake>(makeToGet);
        }

        public async Task<IPagedList<IViewModelVehicleMake>> GetAllAsync(IFiltering filterName, ISorting sort, IPaging page)
        {

            Func<IQueryable<VehicleMake>, IOrderedQueryable<VehicleMake>> orderBy = null;
            Expression<Func<VehicleMake, bool>> filter = null;

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


            if (filterName.SearchName != null)
            {
                filter = s => s.Name.Contains(filterName.SearchName);
            }

            var makeList = await genericRepository.GetAllAsync(filter, orderBy);
            var makePagedList = mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IViewModelVehicleMake>>(makeList);

            return makePagedList.ToPagedList(page.CurrentPage, page.ItemsPerPage);
        }
        public async Task<IEnumerable<IViewModelVehicleMake>> GetAllNoPagingAsync()
        {
            Expression<Func<VehicleMake, bool>> filter = null;
            Func<IQueryable<VehicleMake>, IOrderedQueryable<VehicleMake>> orderBy = null;

            var makeList = await genericRepository.GetAllAsync(filter, orderBy);
            return mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IViewModelVehicleMake>>(makeList);
        }
    }
}
