namespace VehicleService.Model.Common
{
    public interface IPaging
    {
        int CurrentPage { get; set; }
        int ItemsPerPage { get; set; }
    }
}
