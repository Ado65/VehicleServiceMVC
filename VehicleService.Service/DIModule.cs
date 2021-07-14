using VehicleService.DAL.Entity;
using VehicleService.Model;
using VehicleService.Model.Common;
using VehicleService.Repository;
using VehicleService.Repository.Common;
using VehicleService.Service.Common;

namespace VehicleService.Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMake>().To<VehicleMake>();
            Bind<IVehicleModel>().To<VehicleModel>();
            Bind<IFiltering>().To<Filtering>();
            Bind<ISorting>().To<Sorting>();
            Bind<IPaging>().To<Paging>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IMakeService>().To<MakeService>();
            Bind<IViewModelVehicleMake>().To<ViewModelVehicleMake>();
            Bind<IViewModelVehicleModel>().To<ViewModelVehicleModel>();
            Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

        }

    }
}
