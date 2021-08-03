using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleService.DAL.Entity
{
    [Table("VehicleMake")]
    public class VehicleMakeEntity : IVehicleMakeEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModelEntity> VehicleModels { get; set; }
    }
}