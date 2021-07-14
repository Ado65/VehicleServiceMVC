using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleService.Model.Common;

namespace VehicleService.Service.Common
{
    public interface IMakeService
    {
        Task AddAsync(IViewModelVehicleMake makeToAdd);

        Task UpdateAsync(IViewModelVehicleMake makeToUpdate);

        Task DeleteAsync(int id);

        Task<IViewModelVehicleMake> GetByIdAsync(int id);

        Task<IPagedList<IViewModelVehicleMake>> GetAllAsync(IFiltering filterName, ISorting sort, IPaging page);

        Task<IEnumerable<IViewModelVehicleMake>> GetAllNoPagingAsync();

    }
}
