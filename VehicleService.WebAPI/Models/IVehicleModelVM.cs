namespace VehicleService.WebAPI.Models
{
    public interface IVehicleModelVM
    {
        string Abrv { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IVehicleMakeVM VehicleMake { get; set; }
        int VehicleMakeId { get; set; }
    }
}