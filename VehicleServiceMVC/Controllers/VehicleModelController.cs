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
        private readonly IVehicleServiceModel _vehicleServiceModel;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;


        public VehicleModelController(IVehicleServiceModel vehicleServiceModel, IMapper mapper, IVehicleService vehicleService)
        {
            _vehicleServiceModel = vehicleServiceModel;
            _mapper = mapper;
            _vehicleService = vehicleService;
        }

        // GET: VehicleModel
        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MakeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "makeName_desc" : "";
            ViewBag.ArbvSortParm = sortOrder == "arbv" ? "arbv_desc" : "arbv";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";

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

            var model = await _vehicleServiceModel.GetWithPaginationAsync(filtering, sorting, paging);
            var viewModel = _mapper.Map<IPagedList<VehicleModel>, IPagedList<ViewModelVehicleModel>>(model);
            return  View(viewModel);
        }

        // GET: VehicleModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel model = await _vehicleServiceModel.GetByIdAsync(id);
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
            var makeId = await _vehicleService.GetAllAsync();
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
                await _vehicleServiceModel.CreateAsync(model);
                return RedirectToAction("Index");
            }
            var makeId = await _vehicleService.GetAllAsync();
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
            VehicleModel model = await _vehicleServiceModel.GetByIdAsync(id);
            ViewModelVehicleModel viewModel = _mapper.Map<ViewModelVehicleModel>(model);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            var makeId = await _vehicleService.GetAllAsync();
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
                await _vehicleServiceModel.UpdateAsync(model);
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
            VehicleModel model = await _vehicleServiceModel.GetByIdAsync(id);
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
            await _vehicleServiceModel.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
