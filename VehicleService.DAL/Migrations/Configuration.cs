namespace VehicleService.DAL.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VehicleService.DAL.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleService.DAL.VehicleServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VehicleService.DAL.VehicleServiceContext context)
        {
            var make = new List<VehicleMakeEntity>
            {
                new VehicleMakeEntity { Name = "BMW",   Abrv = "",},
                new VehicleMakeEntity { Name = "Peugeot",   Abrv = "",},
                new VehicleMakeEntity { Name = "Audi",   Abrv = "",},
                new VehicleMakeEntity { Name = "Opel",   Abrv = "",},
                new VehicleMakeEntity { Name = "Fiat",   Abrv = "",},
                new VehicleMakeEntity { Name = "Ford",   Abrv = "",},

            };
            make.ForEach(s => context.VehicleMakes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var models = new List<VehicleModelEntity>
            {
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="A4"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="A6"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="R8"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="Q2"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="S8"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="208"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="508"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="2008"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="208"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="1 Series"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="2 Series"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="3 Series"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="X1"
                },
                new VehicleModelEntity {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="X3"
                }
            };
            foreach (VehicleModelEntity e in models)
            {
                var ModoleDataBaseCheck = context.VehicleModels.Where(
                    s => s.VehicleMake.Id == e.VehicleMakeId).FirstOrDefault();
                if (ModoleDataBaseCheck == null)
                {
                    context.VehicleModels.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}
