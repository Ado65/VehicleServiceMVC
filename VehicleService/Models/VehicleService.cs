using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleService.DAL;


namespace VehicleService.Models
{
    public class VehicleService1 : IVehicleService
    {
        private VehicleServiceContext _context;
        public VehicleService1(VehicleServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync()
        {
           return await _context.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> GetByIdAsync(int? id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task CreateAsync(VehicleMake make)
        {

            _context.VehicleMakes.Add(make);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleMake make)
        {
            _context.Entry(make).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            VehicleMake make = await _context.VehicleMakes.FindAsync(id);
            _context.VehicleMakes.Remove(make);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<VehicleMake>> GetWithPaginationAsync(Filtering filterName, Sorting sort, Paging page)
        {
            var make = from m in _context.VehicleMakes
                       select m;

            if (!String.IsNullOrEmpty(filterName.SearchName))
            {
                make = make.Where(n => n.Name.Contains(filterName.SearchName));
            }

            switch (sort.SortOrder)
            {
                case "name_desc":
                    make = make.OrderByDescending(s => s.Name);
                    break;
                case "arbv_desc":
                    make = make.OrderByDescending(s => s.Abrv);
                    break;
                case "arbv":
                    make = make.OrderBy(s => s.Abrv);
                    break;
                default:
                    make = make.OrderBy(s => s.Name);
                    break;
            }

            int totalCount = await make.CountAsync();

            var makeList = await make.Skip(page.ItemsPerPage * (page.CurrentPage - 1)).Take(page.ItemsPerPage).ToListAsync();

            IPagedList<VehicleMake> pageOrders = new StaticPagedList<VehicleMake>(makeList, page.CurrentPage, page.ItemsPerPage, totalCount);
            return pageOrders;
        }      
    }
}

