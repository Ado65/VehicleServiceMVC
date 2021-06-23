using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> MakeGetAllAsync();

        Task<VehicleMake> MakeGetByIdAsync(int? id);

        Task MakeCreateAsync(VehicleMake make);

        Task MakeUpdateAsync(VehicleMake make);

        Task MakeDeleteAsync(int id);

        IEnumerable<VehicleMake> MakeSort(IEnumerable<VehicleMake> makes, string sortOrder);

        Task<IEnumerable<VehicleMake>> MakeFilterAsync(string searchString);

        //VehicleModels

        Task<IEnumerable<VehicleModel>> ModelGetAllAsync();

        Task<VehicleModel> ModelGetByIdAsync(int? id);

        Task ModelCreateAsync(VehicleModel model);

        Task ModelUpdateAsync(VehicleModel model);

        Task ModelDeleteAsync(int? id);
    }
}
