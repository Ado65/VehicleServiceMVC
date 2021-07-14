using System.ComponentModel.DataAnnotations;
using VehicleService.DAL.Entity;

namespace VehicleServiceMVC.Models
{
    public class ViewModelVehicleModel
    {
        public int Id { get; set; }

        [Required]
        public int VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}