using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VehicleService.DAL;

namespace VehicleService.Models
{
    public class VehicleServiceModel : IVehicleServiceModel
    {
        private VehicleServiceContext _context;
        public VehicleServiceModel(VehicleServiceContext context)
        {
            _context = context;
        }
        public async Task<VehicleModel> GetByIdAsync(int? id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task CreateAsync(VehicleModel model)
        {
            _context.VehicleModels.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            VehicleModel model = await _context.VehicleModels.FindAsync(id);
            _context.VehicleModels.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<VehicleModel>> GetWithPaginationAsync(Filtering filterName, ISorting sort, IPaging page)
        {
            var model = from m in _context.VehicleModels
                        select m;

            switch (sort.SortOrder)
            {
                case "makeName_desc":
                    model = model.OrderByDescending(s => s.VehicleMake.Name);
                    break;
                case "arbv_desc":
                    model = model.OrderByDescending(s => s.Abrv);
                    break;
                case "arbv":
                    model = model.OrderBy(s => s.Abrv);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "name":
                    model = model.OrderBy(s => s.Name);
                    break;
                default:
                    model = model.OrderBy(s => s.VehicleMake.Name);
                    break;
            }
            return await model.Where(x => x.VehicleMake.Name.Contains(filterName.SearchName) || filterName.SearchName == null).ToPagedListAsync(page.CurrentPage, page.ItemsPerPage);

        }

    }
}