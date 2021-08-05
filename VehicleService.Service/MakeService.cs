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
        private readonly IUnitOfWork _unitOfWork;

        public MakeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IVehicleMake> GetByIdAsync(int id)
        {
            var makeToGet = await _unitOfWork._VehicleMake.GetByIdAsync(id);
            return makeToGet;
        }

        public async Task<IPagedList<IVehicleMake>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page)
        {
            var makePaged = await _unitOfWork._VehicleMake.GetPagedAsync(filterName, sort, page);
            return makePaged;
        }
        public async Task<IEnumerable<IVehicleMake>> GetAllAsync()
        {

            var makeList = await _unitOfWork._VehicleMake.GetAllAsync();
            return makeList;
        }

        public async Task<int> AddAsync(IVehicleMake makeToAdd)
        {
            var result = await _unitOfWork._VehicleMake.AddAsync(makeToAdd);
            return await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(IVehicleMake makeToUpdate)
        {
            await _unitOfWork._VehicleMake.UpdateAsync(makeToUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork._VehicleMake.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
