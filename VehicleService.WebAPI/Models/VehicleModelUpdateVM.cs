using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleService.WebAPI.Models
{
    public class VehicleModelUpdateVM : IVehicleModelUpdateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MakeName { get; set; }
        public string Abrv { get; set; }

    }
}