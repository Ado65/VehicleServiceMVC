using AutoMapper;
using PagedList;
using VehicleService.DAL.Entity;
using VehicleServiceMVC.Models;

namespace VehicleServiceMVC.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<VehicleModel, ViewModelVehicleModel>().ReverseMap();
            CreateMap<VehicleMake, ViewModelVehicleMake>().ReverseMap();
            CreateMap(typeof(IPagedList<VehicleMake>), typeof(IPagedList<ViewModelVehicleMake>)).ConvertUsing(typeof(PagedListConverter<VehicleMake, ViewModelVehicleMake>));
            CreateMap(typeof(IPagedList<VehicleModel>), typeof(IPagedList<ViewModelVehicleModel>)).ConvertUsing(typeof(PagedListConverter<VehicleModel, ViewModelVehicleModel>));

        }
    }
}