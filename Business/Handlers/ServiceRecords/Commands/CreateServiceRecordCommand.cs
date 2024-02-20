
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
using Business.Handlers.ServiceRecords.ValidationRules;

namespace Business.Handlers.ServiceRecords.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateServiceRecordCommand : IRequest<IResult>
    {

        public System.DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal LaborCost { get; set; }
        public int VehicleId { get; set; }
        public System.Collections.Generic.ICollection<ServiceItem> ServiceItems { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        public class CreateServiceRecordCommandHandler : IRequestHandler<CreateServiceRecordCommand, IResult>
        {
            private readonly IServiceRecordRepository _serviceRecordRepository;
            private readonly IMediator _mediator;
            public CreateServiceRecordCommandHandler(IServiceRecordRepository serviceRecordRepository, IMediator mediator)
            {
                _serviceRecordRepository = serviceRecordRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateServiceRecordValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateServiceRecordCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceRecordRecord = _serviceRecordRepository.Query().Any(u => u.ServiceDate == request.ServiceDate);

                if (isThereServiceRecordRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedServiceRecord = new ServiceRecord
                {
                    ServiceDate = request.ServiceDate,
                    Description = request.Description,
                    LaborCost = request.LaborCost,
                    VehicleId = request.VehicleId,
                    ServiceItems = request.ServiceItems,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    IsDeleted = request.IsDeleted,

                };

                _serviceRecordRepository.Add(addedServiceRecord);
                await _serviceRecordRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}