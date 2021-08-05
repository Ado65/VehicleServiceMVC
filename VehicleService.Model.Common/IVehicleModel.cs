using VehicleService.DAL.Entity;

namespace VehicleService.Model.Common
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IVehicleMake VehicleMake { get; set; }
        int VehicleMakeId { get; set; }
    }
}