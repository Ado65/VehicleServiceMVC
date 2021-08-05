using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleService.WebAPI.Models;

namespace VehicleService.WebAPI.App_Start
{
    public class DIModuleWebAPI : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {

            Bind<IVehicleMakeVM>().To<VehicleMakeVM>();
            Bind<IVehicleModelVM>().To<VehicleModelVM>();
            Bind<IVehicleModelUpdateVM>().To<VehicleModelUpdateVM>();

        }
    }
}