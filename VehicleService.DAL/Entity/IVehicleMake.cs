using System.ComponentModel.DataAnnotations;

namespace VehicleService.DAL.Entity
{
    public interface IVehicleMake
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Abrv { get; set; }

    }
}
