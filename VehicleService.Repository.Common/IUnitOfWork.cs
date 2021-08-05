using System;
using System.Threading.Tasks;

namespace VehicleService.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        IVehicleMakeRepository _VehicleMake { get; }
        IVehicleModelRepository _VehicleModel { get; }
    }
}
