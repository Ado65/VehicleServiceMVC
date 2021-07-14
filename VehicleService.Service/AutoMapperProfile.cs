using AutoMapper;
using VehicleService.DAL.Entity;
using VehicleService.Model;
using VehicleService.Model.Common;

namespace VehicleService.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, IViewModelVehicleMake>().ReverseMap();
            CreateMap<IViewModelVehicleMake, ViewModelVehicleMake>();
        }
    }
}
