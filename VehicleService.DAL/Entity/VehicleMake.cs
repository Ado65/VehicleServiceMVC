using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleService.DAL.Entity
{
    public class VehicleMake : IVehicleMake
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}