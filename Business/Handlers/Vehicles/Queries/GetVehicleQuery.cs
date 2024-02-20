
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Vehicles.Queries
{
    public class GetVehicleQuery : IRequest<IDataResult<Vehicle>>
    {
        public int Id { get; set; }

        public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, IDataResult<Vehicle>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMediator _mediator;

            public GetVehicleQueryHandler(IVehicleRepository vehicleRepository, IMediator mediator)
            {
                _vehicleRepository = vehicleRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Vehicle>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
            {
                var vehicle = await _vehicleRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Vehicle>(vehicle);
            }
        }
    }
}
