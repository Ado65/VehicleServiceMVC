namespace VehicleService.WebAPI.Models
{
    public interface IVehicleModelUpdateVM
    {
        string Abrv { get; set; }
        string MakeName { get; set; }
        string Name { get; set; }
    }
}