using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleService.Model.Common;

namespace VehicleService.Service.Common
{
    public interface IMakeService
    {
        Task<int> AddAsync(IVehicleMake makeToAdd);

        Task UpdateAsync(IVehicleMake makeToUpdate);

        Task DeleteAsync(int id);

        Task<IVehicleMake> GetByIdAsync(int id);

        Task<IPagedList<IVehicleMake>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page);

        Task<IEnumerable<IVehicleMake>> GetAllAsync();

    }
}
