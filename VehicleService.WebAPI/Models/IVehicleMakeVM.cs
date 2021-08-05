namespace VehicleService.WebAPI.Models
{
    public interface IVehicleMakeVM
    {
        string Abrv { get; set; }
        int Id { get; set; }
        string Name { get; set; }
    }
}