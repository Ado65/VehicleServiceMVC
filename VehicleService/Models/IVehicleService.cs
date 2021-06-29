using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetAllAsync();

        Task<VehicleMake> GetByIdAsync(int? id);

        Task CreateAsync(VehicleMake make);

        Task UpdateAsync(VehicleMake make);

        Task DeleteAsync(int id);

        Task<IPagedList<VehicleMake>> GetWithPaginationAsync(Filtering filterName, Sorting sort, Paging page);


    }
}
