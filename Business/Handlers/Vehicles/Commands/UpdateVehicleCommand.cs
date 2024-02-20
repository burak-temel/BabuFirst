
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Vehicles.ValidationRules;


namespace Business.Handlers.Vehicles.Commands
{


    public class UpdateVehicleCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public int CustomerId { get; set; }
        public System.Collections.Generic.ICollection<ServiceRecord> ServiceRecords { get; set; }

        public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, IResult>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMediator _mediator;

            public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository, IMediator mediator)
            {
                _vehicleRepository = vehicleRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateVehicleValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
            {
                var isThereVehicleRecord = await _vehicleRepository.GetAsync(u => u.Id == request.Id);


                isThereVehicleRecord.LicensePlate = request.LicensePlate;
                isThereVehicleRecord.Make = request.Make;
                isThereVehicleRecord.Model = request.Model;
                isThereVehicleRecord.Year = request.Year;
                isThereVehicleRecord.VIN = request.VIN;
                isThereVehicleRecord.Mileage = request.Mileage;
                isThereVehicleRecord.CustomerId = request.CustomerId;
                isThereVehicleRecord.ServiceRecords = request.ServiceRecords;


                _vehicleRepository.Update(isThereVehicleRecord);
                await _vehicleRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

