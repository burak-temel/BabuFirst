
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Vehicles.ValidationRules;

namespace Business.Handlers.Vehicles.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateVehicleCommand : IRequest<IResult>
    {

        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public int CustomerId { get; set; }
        public System.Collections.Generic.ICollection<ServiceRecord> ServiceRecords { get; set; }


        public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, IResult>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMediator _mediator;
            public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository, IMediator mediator)
            {
                _vehicleRepository = vehicleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateVehicleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
            {
                var isThereVehicleRecord = _vehicleRepository.Query().Any(u => u.LicensePlate == request.LicensePlate);

                if (isThereVehicleRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedVehicle = new Vehicle
                {
                    LicensePlate = request.LicensePlate,
                    Make = request.Make,
                    Model = request.Model,
                    Year = request.Year,
                    VIN = request.VIN,
                    Mileage = request.Mileage,
                    CustomerId = request.CustomerId,
                    ServiceRecords = request.ServiceRecords,

                };

                _vehicleRepository.Add(addedVehicle);
                await _vehicleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}