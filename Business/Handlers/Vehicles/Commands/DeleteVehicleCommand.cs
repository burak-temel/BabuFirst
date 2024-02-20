
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Vehicles.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteVehicleCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, IResult>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMediator _mediator;

            public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository, IMediator mediator)
            {
                _vehicleRepository = vehicleRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
            {
                var vehicleToDelete = _vehicleRepository.Get(p => p.Id == request.Id);

                _vehicleRepository.Delete(vehicleToDelete);
                await _vehicleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

