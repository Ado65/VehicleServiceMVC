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

        public MakeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(IVehicleMake makeToAdd)
        {
            var result =await unitOfWork.VehicleMake.AddAsync(makeToAdd);
            return await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(IVehicleMake makeToUpdate)
        {
            await unitOfWork.VehicleMake.UpdateAsync(makeToUpdate);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.VehicleMake.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public async Task<IVehicleMake> GetByIdAsync(int id)
        {
            var makeToGet = await unitOfWork.VehicleMake.GetByIdAsync(id);
            return makeToGet;
        }

        public async Task<IPagedList<IVehicleMake>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page)
        {
            var makePaged = await unitOfWork.VehicleMake.GetPagedAsync(filterName, sort, page);
            return makePaged;
        }
        public async Task<IEnumerable<IVehicleMake>> GetAllAsync()
        {

            var makeList = await unitOfWork.VehicleMake.GetAllAsync();
            return makeList;
        }
    }
}
