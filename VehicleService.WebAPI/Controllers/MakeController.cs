using AutoMapper;
using System.Collections.Generic;
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
        public async Task<IEnumerable<IViewModelVehicleMake>> GetAllNoPagingAsync()
        {
            return await makeService.GetAllNoPagingAsync();
        }

        // GET api/make/paging?page=&sortOrder=&searchString=
        [HttpGet]
        [Route("api/make/paging")]
        public async Task<IEnumerable<IViewModelVehicleMake>> GetAllAsync(int? page, string sortOrder, string searchString)
        {
            //Note: Sorting- "name_desc", "arbv_desc", "arbv"
            paging.CurrentPage = (page ?? 1);
            paging.ItemsPerPage = 5;
            filtering.SearchName = searchString;
            sorting.SortOrder = sortOrder;
            return await makeService.GetAllAsync(filtering, sorting, paging);
        }

        // GET api/make/5
        [HttpGet]
        public async Task<IViewModelVehicleMake> Get(int id)
        {
            var make = await makeService.GetByIdAsync(id);
            return make;
        }

        // POST api/make
        [HttpPost]
        public async Task Post(ViewModelVehicleMake makeValue)
        {
            await makeService.AddAsync(mapper.Map<ViewModelVehicleMake, IViewModelVehicleMake>(makeValue));
        }

        // PUT api/make
        [HttpPut]
        public async Task Put(ViewModelVehicleMake makeValue)
        {

            await makeService.UpdateAsync(mapper.Map<ViewModelVehicleMake, IViewModelVehicleMake>(makeValue));
        }

        // DELETE api/make/5
        [HttpDelete]
        public async Task Delete(int id)
        {
            await makeService.DeleteAsync(id);
        }
    }
}
