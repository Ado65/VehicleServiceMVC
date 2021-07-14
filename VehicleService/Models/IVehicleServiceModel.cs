using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public interface IVehicleServiceModel
    {
        Task<VehicleModel> GetByIdAsync(int? id);

        Task CreateAsync(VehicleModel model);

        Task UpdateAsync(VehicleModel model);

        Task DeleteAsync(int? id);

        Task<IPagedList<VehicleModel>> GetWithPaginationAsync(Filtering filterName, ISorting sort, IPaging page);

    }
}
