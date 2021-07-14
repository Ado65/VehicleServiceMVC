using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using VehicleService.Service.Common;
using VehicleService.Model;
using VehicleService.DAL.Entity;
using ViewModelVehicleMake = VehicleService.Model.ViewModelVehicleMake;

namespace VehicleServiceMVC.Controllers
{
    public class VehicleMakeController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IMakeService _makeService;
        public VehicleMakeController( IMapper mapper, IMakeService makeService)
        {
            _mapper = mapper;
            _makeService = makeService;
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

            filtering.SearchName = searchString;
            sorting.SortOrder = sortOrder;
            paging.CurrentPage = (page ?? 1);
            paging.ItemsPerPage = 5;

            var make = await _makeService.GetAllAsync(filtering, sorting, paging);

            //var viewMake = _mapper.Map<IPagedList<VehicleMake>, IPagedList<ViewModelVehicleMake>>(make);

            return View(make);
        }

        // GET: VehicleMake/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _makeService.GetByIdAsync(id);

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
                await _makeService.AddAsync(make);
                return RedirectToAction("Index");
            }

            return View(viewMake);
        }


        // GET: VehicleMake/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _makeService.GetByIdAsync(id);
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
                await _makeService.UpdateAsync(make);
                return RedirectToAction("Index");
            }
            return View(viewMake);
        }
        // GET: VehicleMake/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _makeService.GetByIdAsync(id);
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
            await _makeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
