using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleService.Models;
using VehicleServiceMVC.Models;

namespace VehicleServiceMVC.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, ViewModelVehicleMake>().ReverseMap();
            CreateMap<VehicleModel, ViewModelVehicleModel>().ReverseMap();
            CreateMap(typeof(IPagedList<VehicleMake>), typeof(IPagedList<ViewModelVehicleMake>)).ConvertUsing(typeof(PagedListConverter<VehicleMake, ViewModelVehicleMake>));
            CreateMap(typeof(IPagedList<VehicleModel>), typeof(IPagedList<ViewModelVehicleModel>)).ConvertUsing(typeof(PagedListConverter<VehicleModel, ViewModelVehicleModel>));

        }
    }
}