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
            var make = new List<VehicleMake>
            {
                new VehicleMake { Name = "BMW",   Abrv = "",},
                new VehicleMake { Name = "Peugeot",   Abrv = "",},
                new VehicleMake { Name = "Audi",   Abrv = "",},
                new VehicleMake { Name = "Opel",   Abrv = "",},
                new VehicleMake { Name = "Fiat",   Abrv = "",},
                new VehicleMake { Name = "Ford",   Abrv = "",},

            };
            make.ForEach(s => context.VehicleMakes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var models = new List<VehicleModel>
            {
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="A4"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="A6"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="R8"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="Q2"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Audi").Id,
                    Name="S8"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="208"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="508"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="2008"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "Peugeot").Id,
                    Name="208"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="1 Series"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="2 Series"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="3 Series"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="X1"
                },
                new VehicleModel {
                    VehicleMakeId = make.Single(s => s.Name == "BMW").Id,
                    Name="X3"
                }
            };
            foreach (VehicleModel e in models)
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
