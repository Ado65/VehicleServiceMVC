using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleService.Model.Common;

namespace VehicleService.Service.Common
{
    public interface IModelService
    {
        Task<IVehicleModel> GetByIdAsync(int id);
        Task<IPagedList<IVehicleModel>> GetPagedAsync(IFiltering filterName, ISorting sort, IPaging page);
        Task<IEnumerable<IVehicleModel>> GetAllAsync();
        Task AddAsync(IVehicleModel modelToAdd, IVehicleMake makeToCheck);
        Task UpdateAsync(IVehicleModel modelToUpdate);
        Task DeleteAsync(int id);
    }
}
