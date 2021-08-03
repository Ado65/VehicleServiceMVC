using System.ComponentModel.DataAnnotations;

namespace VehicleService.DAL.Entity
{
    public interface IVehicleMakeEntity
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Abrv { get; set; }

    }
}
