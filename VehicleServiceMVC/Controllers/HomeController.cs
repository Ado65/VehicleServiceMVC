using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VehicleService.Models;
using VehicleServiceMVC.Models;

namespace VehicleServiceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        public HomeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
          //  ViewBag.Message = "Your application description page.";
          //  VehicleMake make = _vehicleService.Read();
          //  ViewModelVehicleMake viewMake = _mapper.Map<ViewModelVehicleMake>(make);
            return View();
        }



    }
}