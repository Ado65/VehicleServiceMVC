namespace VehicleService.Model.Common
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IVehicleMake ViewModelVehicleMake { get; set; }
        int VehicleMakeId { get; set; }
    }
}