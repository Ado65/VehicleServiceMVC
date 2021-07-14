namespace VehicleService.Model.Common
{
    public interface IViewModelVehicleModel
    {
        string Abrv { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IViewModelVehicleMake ViewModelVehicleMake { get; set; }
        int VehicleMakeId { get; set; }
    }
}