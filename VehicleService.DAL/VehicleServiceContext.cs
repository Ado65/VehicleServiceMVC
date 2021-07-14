using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VehicleService.DAL.Entity;

namespace VehicleService.DAL
{
    public class VehicleServiceContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}