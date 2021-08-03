using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleService.Model;
using VehicleService.Model.Common;
using VehicleService.Service.Common;

namespace VehicleService.WebAPI.Controllers
{
    public class MakeController : ApiController
    {
        private readonly IMakeService makeService;
        private readonly IMapper mapper;
        private IPaging paging;
        private IFiltering filtering;
        private ISorting sorting;
        public MakeController(IMakeService makeService, IMapper mapper, IPaging paging, IFiltering filtering, ISorting sorting)
        {
            this.makeService = makeService;
            this.mapper = mapper;
            this.paging = paging;
            this.filtering = filtering;
            this.sorting = sorting;
        }

        // GET api/make
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var makeAll = await makeService.GetAllAsync();
            if (makeAll == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, makeAll);
        }

        // GET api/make/paging?page=&sortOrder=&searchString=
        [HttpGet]
        [Route("api/make/paging")]
        public async Task<HttpResponseMessage> GetPaged(int? page, string sortOrder, string searchString)
        {
            //Note: Sorting- "name_desc", "arbv_desc", "arbv"
            paging.CurrentPage = (page ?? 1);
            paging.ItemsPerPage = 5;
            filtering.SearchName = searchString;
            sorting.SortOrder = sortOrder;

            var makePaged= await makeService.GetPagedAsync(filtering, sorting, paging);

            if (makePaged == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, makePaged);

        }

        // GET api/make/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var make = await makeService.GetByIdAsync(id);
            if (make == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, make);
        }

        // POST api/make
        [HttpPost]
        public async Task<HttpResponseMessage> Post(VehicleMake makeValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            await makeService.AddAsync(mapper.Map<VehicleMake, IVehicleMake>(makeValue));

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/make
        [HttpPut]
        public async Task<HttpResponseMessage> Put(VehicleMake makeValue)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            if (await makeService.GetByIdAsync(makeValue.Id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await makeService.UpdateAsync(mapper.Map<VehicleMake, IVehicleMake>(makeValue));
            return Request.CreateResponse(HttpStatusCode.NoContent);

        }

        // DELETE api/make/5
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (await makeService.GetByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await makeService.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
