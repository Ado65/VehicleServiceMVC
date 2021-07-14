using System.ComponentModel.DataAnnotations;
using VehicleService.Model.Common;

namespace VehicleService.Model
{
    public class ViewModelVehicleModel : IViewModelVehicleModel
    {
        public int Id { get; set; }

        [Required]
        public int VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual IViewModelVehicleMake ViewModelVehicleMake { get; set; }

    }
}