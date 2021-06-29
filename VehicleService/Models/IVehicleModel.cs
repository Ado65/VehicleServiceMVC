using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Models
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
