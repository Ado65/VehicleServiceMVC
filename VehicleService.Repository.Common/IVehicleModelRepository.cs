using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Model.Common;

namespace VehicleService.Repository.Common
{
    public interface IVehicleModelRepository
    {
        Task<IVehicleModel> GetByIdAsync(int id);
        Task<IEnumerable<IVehicleModel>> GetAllAsync();
        Task<IPagedList<IVehicleModel>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page);
        Task<int> AddAsync(IVehicleModel vehicleModleAdd);
        Task<int> UpdateAsync(IVehicleModel vehicleModelUpdate);
        Task<int> DeleteAsync(int id);
    }
}
