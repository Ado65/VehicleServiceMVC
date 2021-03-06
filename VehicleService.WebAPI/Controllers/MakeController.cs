using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleService.Model;
using VehicleService.Model.Common;
using VehicleService.Service.Common;
using VehicleService.WebAPI.Models;

namespace VehicleService.WebAPI.Controllers
{
    public class MakeController : ApiController
    {
        private readonly IMakeService _makeService;
        private readonly IMapper _mapper;
        private IPaging _paging;
        private IFiltering _filtering;
        private ISorting _sorting;
        public MakeController(IMakeService makeService, IMapper mapper, IPaging paging, IFiltering filtering, ISorting sorting)
        {
            _makeService = makeService;
            _mapper = mapper;
            _paging = paging;
            _filtering = filtering;
            _sorting = sorting;
        }

        // GET api/make
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var makeAll = await _makeService.GetAllAsync();
            if (makeAll == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var makeAllVM = _mapper.Map<IEnumerable<IVehicleMakeVM>>(makeAll);
            return Request.CreateResponse(HttpStatusCode.OK, makeAllVM);
        }

        // GET api/make/paging?page=&sortOrder=&searchString=
        [HttpGet]
        [Route("api/make/paging")]
        public async Task<HttpResponseMessage> GetPaged(int? page, string sortOrder, string searchString)
        {
            //Note: Sorting- "name_desc", "arbv_desc", "arbv"
            _paging.CurrentPage = (page ?? 1);
            _paging.ItemsPerPage = 5;
            _filtering.SearchName = searchString;
            _sorting.SortOrder = sortOrder;

            var makePaged= await _makeService.GetPagedAsync(_filtering, _sorting, _paging);

            if (makePaged == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var makePagedVM = _mapper.Map<IPagedList<IVehicleMakeVM>>(makePaged);
            return Request.CreateResponse(HttpStatusCode.OK, makePagedVM);

        }

        // GET api/make/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var make = await _makeService.GetByIdAsync(id);
            if (make == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var makeVM = _mapper.Map<IVehicleMakeVM>(make);
            return Request.CreateResponse(HttpStatusCode.OK, makeVM);
        }

        // POST api/make
        [HttpPost]
        public async Task<HttpResponseMessage> Post(VehicleMake makeValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            await _makeService.AddAsync(_mapper.Map<VehicleMake, IVehicleMake>(makeValue));

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/make/5
        [HttpPut]
        public async Task<HttpResponseMessage> Put(int id, VehicleMake makeValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            //var makeToUpdate = await _makeService.GetByIdAsync(id);
            //if (makeToUpdate == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            await _makeService.UpdateAsync(_mapper.Map<IVehicleMake>(makeValue));
            return Request.CreateResponse(HttpStatusCode.NoContent);

        }

        // DELETE api/make/5
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (await _makeService.GetByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await _makeService.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
