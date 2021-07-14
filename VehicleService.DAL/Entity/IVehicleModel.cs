using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleService.DAL.Entity
{
    public interface IVehicleModel
    {
        int Id { get; set; }
        [Column("MakeId")]
        [Required]
        int VehicleMakeId { get; set; }
        [Required]
        string Name { get; set; }
        string Abrv { get; set; }
    }
}
