using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VehicleService.DAL;


namespace VehicleService.Models
{
    public class VehicleService1 : IVehicleService
    {
        private   VehicleServiceContext _contex;
        public VehicleService1(VehicleServiceContext context)
        {
            _contex = context;
        }

        public async Task MakeCreateAsync(VehicleMake make)
        {

            _contex.VehicleMakes.Add(make);
            await _contex.SaveChangesAsync();
        }

        public async Task MakeDeleteAsync(int id)
        {
            VehicleMake make = await _contex.VehicleMakes.FindAsync(id);
            _contex.VehicleMakes.Remove(make);
            await _contex.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleMake>> MakeGetAllAsync()
        {
            return await _contex.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> MakeGetByIdAsync(int? id)
        {
            return await _contex.VehicleMakes.FindAsync(id);
        }


        public async Task MakeUpdateAsync(VehicleMake make)
        {
            _contex.Entry(make).State = EntityState.Modified;
            await _contex.SaveChangesAsync();
        }

        public  IEnumerable<VehicleMake> MakeSort(IEnumerable<VehicleMake> makes, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    makes =  makes.OrderByDescending(s => s.Name);
                    break;
                case "arbv_desc":
                    makes = makes.OrderByDescending(s => s.Abrv);
                    break;
                case "arbv":
                    makes = makes.OrderBy(s => s.Abrv);
                    break;
                default:
                    makes = makes.OrderBy(s => s.Name);
                    break;
            }
            return  makes;
        }

        public async Task<IEnumerable<VehicleMake>> MakeFilterAsync(string searchString)
        {
            var make = from m in _contex.VehicleMakes
                         select m;
            make = make.Where(n => n.Name.Contains(searchString));
            return await make.ToListAsync();
        }

        //VehicleModels

        public async Task ModelCreateAsync(VehicleModel model)
        {
            _contex.VehicleModels.Add(model);
            await _contex.SaveChangesAsync();
        }

        public async Task ModelDeleteAsync(int? id)
        {
            VehicleModel model = await _contex.VehicleModels.FindAsync(id);
            _contex.VehicleModels.Remove(model);
            await _contex.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleModel>> ModelGetAllAsync()
        {
            return await _contex.VehicleModels.ToListAsync();
        }

        public async Task<VehicleModel> ModelGetByIdAsync(int? id)
        {
            return await _contex.VehicleModels.FindAsync(id);
        }

        public async Task ModelUpdateAsync(VehicleModel model)
        {
            _contex.Entry(model).State = EntityState.Modified;
            await _contex.SaveChangesAsync();
        }

    }
}