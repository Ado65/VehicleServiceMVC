using AutoMapper;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Transactions;
using VehicleService.DAL;
using VehicleService.Repository.Common;

namespace VehicleService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VehicleServiceContext _DbContext { get; private set; }
        public IVehicleMakeRepository _VehicleMake { get; }
        public IVehicleModelRepository _VehicleModel { get; }
        private IMapper _mapper;

        public UnitOfWork(VehicleServiceContext dbContext, IMapper mapper)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            _DbContext = dbContext;
            _mapper = mapper;
            _VehicleMake = new VehicleMakeRepository(_DbContext,_mapper);
            _VehicleModel = new VehicleModelRepository(_DbContext, _mapper);
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _DbContext.SaveChangesAsync();
                scope.Complete();
            }           
            return result;
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
