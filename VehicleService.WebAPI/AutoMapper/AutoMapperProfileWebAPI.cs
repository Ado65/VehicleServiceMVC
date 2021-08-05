using AutoMapper;
using PagedList;
using VehicleService.Model.Common;
using VehicleService.WebAPI.Models;

namespace VehicleService.WebAPI.AutoMapper
{
    public class AutoMapperProfileWebAPI : Profile
    {
        public AutoMapperProfileWebAPI()
        {
            CreateMap<VehicleModelUpdateVM, IVehicleModel>();
            CreateMap<VehicleModelUpdateVM, IVehicleMake>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.MakeName));
            CreateMap<VehicleMakeVM, IVehicleMake>().ReverseMap();
            CreateMap<VehicleModelVM, IVehicleModel>().ReverseMap();
            CreateMap<IVehicleMakeVM, IVehicleMake>().ReverseMap();
            CreateMap<IVehicleModelVM, IVehicleModel>().ReverseMap();

            CreateMap(typeof(IPagedList<IVehicleMake>), typeof(IPagedList<IVehicleMakeVM>)).ConvertUsing(typeof(PagedListConverter<IVehicleMake, IVehicleMakeVM>));
            CreateMap(typeof(IPagedList<IVehicleModel>), typeof(IPagedList<IVehicleModelVM>)).ConvertUsing(typeof(PagedListConverter<IVehicleModel, IVehicleModelVM>));
        }
    }
}