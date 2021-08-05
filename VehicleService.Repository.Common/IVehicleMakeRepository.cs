using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Model;
using VehicleService.Model.Common;

namespace VehicleService.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IVehicleMake> GetByIdAsync(int id);
        Task<IVehicleMake> GetByNameAsync(string targetName);
        Task<IEnumerable<IVehicleMake>> GetAllAsync();
        Task<IPagedList<IVehicleMake>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page);
        Task<int> AddAsync(IVehicleMake vehicleMakeAdd);
        Task<int> UpdateAsync(IVehicleMake vehicleMakeUpdate);
        Task<int> DeleteAsync(int id);
    }
}
