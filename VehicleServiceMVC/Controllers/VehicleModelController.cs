using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VehicleService.Models;
using VehicleServiceMVC.Models;

namespace VehicleServiceMVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: VehicleModel
        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MakeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "makeName_desc" : "";
            ViewBag.ArbvSortParm = sortOrder == "arbv" ? "arbv_desc" : "arbv";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            IEnumerable<VehicleModel> model = await _vehicleService.ModelGetAllAsync();


            if (!String.IsNullOrEmpty(searchString))
            {
                model = await _vehicleService.ModelFilterAsync(searchString);
            }

            model = _vehicleService.ModelSort(model, sortOrder);



            IEnumerable<ViewModelVehicleModel> viewModel = _mapper.Map<IEnumerable<VehicleModel>, IEnumerable<ViewModelVehicleModel>>(model);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(viewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = await _vehicleService.ModelGetByIdAsync(id);
            ViewModelVehicleModel viewModel = _mapper.Map<ViewModelVehicleModel>(model);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: VehicleModel/Create
        public async Task<ActionResult> Create()
        {
            var makeId = await _vehicleService.MakeGetAllAsync();
            IEnumerable<ViewModelVehicleMake> viewMakeId = _mapper.Map<IEnumerable<VehicleMake>, IEnumerable<ViewModelVehicleMake>>(makeId);
            ViewBag.VehicleMakeId = new SelectList(viewMakeId, "Id", "Name");
            return View();
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] ViewModelVehicleModel viewModel)
        {
            if(ModelState.IsValid)
            {
               VehicleModel model = _mapper.Map<VehicleModel>(viewModel);
                await _vehicleService.ModelCreateAsync(model);
                return RedirectToAction("Index");
            }
            var makeId = await _vehicleService.MakeGetAllAsync();
            IEnumerable<ViewModelVehicleMake> viewMakeId = _mapper.Map<IEnumerable<VehicleMake>, IEnumerable<ViewModelVehicleMake>>(makeId);
            ViewBag.VehicleMakeId = new SelectList(viewMakeId, "Id", "Name"); ;
            return View(viewModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = await _vehicleService.ModelGetByIdAsync(id);
            ViewModelVehicleModel viewModel = _mapper.Map<ViewModelVehicleModel>(model);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            var makeId = await _vehicleService.MakeGetAllAsync();
            IEnumerable<ViewModelVehicleMake> viewMakeId = _mapper.Map<IEnumerable<VehicleMake>, IEnumerable<ViewModelVehicleMake>>(makeId);
            ViewBag.VehicleMakeId = new SelectList(viewMakeId, "Id", "Name");
            return View(viewModel);
        }

        // POST: VehicleModel/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VehicleMakeId,Name,Abrv")] ViewModelVehicleModel viewModel)
        {
            if(ModelState.IsValid)
            {
                VehicleModel model = _mapper.Map<VehicleModel>(viewModel);
                await _vehicleService.ModelUpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: VehicleModel/Delete/5
        public async  Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = await _vehicleService.ModelGetByIdAsync(id);
            ViewModelVehicleModel viewModel = _mapper.Map<ViewModelVehicleModel>(model);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _vehicleService.ModelDeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
