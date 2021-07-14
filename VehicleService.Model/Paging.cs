using VehicleService.Model.Common;

namespace VehicleService.Model
{
    public class Paging : IPaging
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}