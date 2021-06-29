using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleServiceMVC.Models;
using VehicleService.Models;
using AutoMapper;
using PagedList;

namespace VehicleServiceMVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleMake
        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ArbvSortParm = sortOrder == "arbv" ? "arbv_desc" : "arbv";

            var filtering = new Filtering();
            var sorting = new Sorting();
            var paging = new Paging();

            if (searchString != null)
            {
               page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            filtering.searchName = searchString;
            sorting.sortOrder = sortOrder;
            paging.currentPage = (page ?? 1);
            paging.itemsPerPage = 5;

            var make = await _vehicleService.GetWithPaginationAsync(filtering, sorting, paging);

            var viewMake = _mapper.Map<IPagedList<VehicleMake>, IPagedList<ViewModelVehicleMake>>(make);

            return View(viewMake);
        }

        // GET: VehicleMake/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _vehicleService.GetByIdAsync(id);
            ViewModelVehicleMake viewMake = _mapper.Map<ViewModelVehicleMake>(make);
            if (viewMake == null)
            {
                return HttpNotFound();
            }
            return View(viewMake);
        }

        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv")] ViewModelVehicleMake viewMake)
        {
            if (ModelState.IsValid)
            {
                VehicleMake make = _mapper.Map<VehicleMake>(viewMake);
                await _vehicleService.CreateAsync(make);
                return RedirectToAction("Index");
            }

            return View(viewMake);
        }


        // GET: VehicleMake/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _vehicleService.GetByIdAsync(id);
            ViewModelVehicleMake viewMake = _mapper.Map<ViewModelVehicleMake>(make);
            if (viewMake == null)
            {
                return HttpNotFound();
            }
            return View(viewMake);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Abrv")] ViewModelVehicleMake viewMake)
        {
            if (ModelState.IsValid)
            {
                VehicleMake make = _mapper.Map<VehicleMake>(viewMake);
                await _vehicleService.UpdateAsync(make);
                return RedirectToAction("Index");
            }
            return View(viewMake);
        }
        // GET: VehicleMake/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _vehicleService.GetByIdAsync(id);
            ViewModelVehicleMake viewMake = _mapper.Map<ViewModelVehicleMake>(make);
            if (viewMake == null)
            {
                return HttpNotFound();
            }
            return View(viewMake);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
