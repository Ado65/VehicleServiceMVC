using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public interface IVehicleMake
    {
         int Id { get; set; }
        [Required]
         string Name { get; set; }
         string Abrv { get; set; }

    }
}
