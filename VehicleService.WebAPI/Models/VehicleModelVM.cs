using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleService.WebAPI.Models
{
    public class VehicleModelVM : IVehicleModelVM
    {
        public int Id { get; set; }
        [Required]
        public int VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual IVehicleMakeVM VehicleMake { get; set; }
    }
}