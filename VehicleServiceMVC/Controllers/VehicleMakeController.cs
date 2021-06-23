using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleServiceMVC.Models;
using VehicleService.Models;
using AutoMapper;

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
        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ArbvSortParm = sortOrder == "arbv" ? "arbv_desc" : "arbv";

            var make = await _vehicleService.MakeGetAllAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                make = await _vehicleService.MakeFilterAsync(searchString);
            }

            make =  _vehicleService.MakeSort(make, sortOrder);

            IEnumerable<ViewModelVehicleMake> viewMake = _mapper.Map<IEnumerable<VehicleMake>, IEnumerable<ViewModelVehicleMake>>(make);
 
            return View(viewMake);
        }

        // GET: VehicleMake/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake make = await _vehicleService.MakeGetByIdAsync(id);
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
                await _vehicleService.MakeCreateAsync(make);
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
            VehicleMake make = await _vehicleService.MakeGetByIdAsync(id);
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
                await _vehicleService.MakeUpdateAsync(make);
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
            VehicleMake make = await _vehicleService.MakeGetByIdAsync(id);
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
            await _vehicleService.MakeDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
