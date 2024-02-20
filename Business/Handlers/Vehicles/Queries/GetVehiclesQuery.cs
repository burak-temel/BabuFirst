
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Vehicles.Queries
{

    public class GetVehiclesQuery : IRequest<IDataResult<IEnumerable<Vehicle>>>
    {
        public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IDataResult<IEnumerable<Vehicle>>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMediator _mediator;

            public GetVehiclesQueryHandler(IVehicleRepository vehicleRepository, IMediator mediator)
            {
                _vehicleRepository = vehicleRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Vehicle>>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Vehicle>>(await _vehicleRepository.GetListAsync());
            }
        }
    }
}