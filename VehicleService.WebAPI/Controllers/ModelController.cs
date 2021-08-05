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
    public class ModelController : ApiController
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;
        private IPaging _paging;
        private IFiltering _filtering;
        private ISorting _sorting;
        public ModelController(IModelService modelService, IMapper mapper, IPaging paging, IFiltering filtering, ISorting sorting)
        {
            _modelService = modelService;
            _mapper = mapper;
            _paging = paging;
            _filtering = filtering;
            _sorting = sorting;
        }

        // GET api/model
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var modelAll = await _modelService.GetAllAsync();
            if (modelAll == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var modelAllVM = _mapper.Map<IEnumerable<IVehicleModelVM>>(modelAll);
            return Request.CreateResponse(HttpStatusCode.OK, modelAllVM);
        }

        // GET api/model/paging?page=&sortOrder=&searchString=
        [HttpGet]
        [Route("api/model/paging")]
        public async Task<HttpResponseMessage> GetPaged(int? page, string sortOrder, string searchString)
        {
            //Note: Sorting- "name_desc", "arbv_desc", "arbv"
            _paging.CurrentPage = (page ?? 1);
            _paging.ItemsPerPage = 5;
            _filtering.SearchName = searchString;
            _sorting.SortOrder = sortOrder;

            var modelPaged = await _modelService.GetPagedAsync(_filtering, _sorting, _paging);

            if (modelPaged == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var modelPagedVM = _mapper.Map<IPagedList<IVehicleModelVM>>(modelPaged);
            return Request.CreateResponse(HttpStatusCode.OK, modelPagedVM);

        }

        // GET api/model/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var model = await _modelService.GetByIdAsync(id);
            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var modelVM = _mapper.Map<IVehicleModelVM>(model);
            return Request.CreateResponse(HttpStatusCode.OK, modelVM);
        }
        // POST api/model
        [HttpPost]
        public async Task<HttpResponseMessage> Post(VehicleModelUpdateVM modelValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var modelToAdd = _mapper.Map<IVehicleModel>(modelValue);
            var makeToCheck = _mapper.Map<IVehicleMake>(modelValue);
            await _modelService.AddAsync(modelToAdd, makeToCheck);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/model
        [HttpPut]
        public async Task<HttpResponseMessage> Put(VehicleModel modelValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            //var modelToUpdate = await _modelService.GetByIdAsync(modelValue.Id);
            //if (modelToUpdate == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            await _modelService.UpdateAsync(_mapper.Map<IVehicleModel>(modelValue));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // DELETE api/model/5
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (await _modelService.GetByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await _modelService.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}