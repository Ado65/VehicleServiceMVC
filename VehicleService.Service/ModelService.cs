using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Model.Common;
using VehicleService.Repository.Common;
using VehicleService.Service.Common;

namespace VehicleService.Service
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IVehicleModel> GetByIdAsync(int id)
        {
            var modelToGet = await _unitOfWork._VehicleModel.GetByIdAsync(id);
            return modelToGet;
        }

        public async Task<IEnumerable<IVehicleModel>> GetAllAsync()
        {
            var modelList = await _unitOfWork._VehicleModel.GetAllAsync();
            return modelList;
        }

        public async Task<IPagedList<IVehicleModel>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page)
        {
            var modelPaged = await _unitOfWork._VehicleModel.GetPagedAsync(filterName, sort, page);
            return modelPaged;
        }

        public async Task AddAsync(IVehicleModel modelToAdd, IVehicleMake makeToCheck)
        {
            var check = await _unitOfWork._VehicleMake.GetByNameAsync(makeToCheck.Name);
            if (check == null)
            {
                modelToAdd.VehicleMake = makeToCheck;
            }
            else
            {
                modelToAdd.VehicleMakeId = check.Id;
            }
            await _unitOfWork._VehicleModel.AddAsync(modelToAdd);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(IVehicleModel modelToUpdate)
        {
            await _unitOfWork._VehicleModel.UpdateAsync(modelToUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork._VehicleModel.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
