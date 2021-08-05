using VehicleService.DAL.Entity;
using VehicleService.Model;
using VehicleService.Model.Common;
using VehicleService.Repository;
using VehicleService.Repository.Common;
using VehicleService.Service.Common;
using Ninject.Web.Common;

namespace VehicleService.Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeEntity>().To<VehicleMakeEntity>();
            Bind<IVehicleModelEntity>().To<VehicleModelEntity>();
            Bind<IFiltering>().To<Filtering>();
            Bind<ISorting>().To<Sorting>();
            Bind<IPaging>().To<Paging>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IMakeService>().To<MakeService>();
            Bind<IVehicleMake>().To<VehicleMake>();
            Bind<IVehicleModel>().To<VehicleModel>();
            Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            Bind<IModelService>().To<ModelService>();

        }

    }
}
