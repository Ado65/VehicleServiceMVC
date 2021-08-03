using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VehicleService.DAL.Entity;

namespace VehicleService.DAL
{
    public class VehicleServiceContext : DbContext
    {
        public DbSet<VehicleMakeEntity> VehicleMakes { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}