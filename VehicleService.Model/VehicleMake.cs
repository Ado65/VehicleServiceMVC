using System.ComponentModel.DataAnnotations;
using VehicleService.Model.Common;

namespace VehicleService.Model
{
    public class VehicleMake : IVehicleMake
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

    }
}