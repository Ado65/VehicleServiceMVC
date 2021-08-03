using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleService.DAL.Entity
{
    [Table("VehicleModel")]
    public class VehicleModelEntity : IVehicleModelEntity
    {
        public int Id { get; set; }
        [Column("MakeId")]
        [Required]
        public int VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMakeEntity VehicleMake { get; set; }
    }
}