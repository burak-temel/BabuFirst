
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
using Business.Handlers.ServiceRecords.ValidationRules;


namespace Business.Handlers.ServiceRecords.Commands
{


    public class UpdateServiceRecordCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal LaborCost { get; set; }
        public int VehicleId { get; set; }
        public System.Collections.Generic.ICollection<ServiceItem> ServiceItems { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public class UpdateServiceRecordCommandHandler : IRequestHandler<UpdateServiceRecordCommand, IResult>
        {
            private readonly IServiceRecordRepository _serviceRecordRepository;
            private readonly IMediator _mediator;

            public UpdateServiceRecordCommandHandler(IServiceRecordRepository serviceRecordRepository, IMediator mediator)
            {
                _serviceRecordRepository = serviceRecordRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateServiceRecordValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateServiceRecordCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceRecordRecord = await _serviceRecordRepository.GetAsync(u => u.Id == request.Id);


                isThereServiceRecordRecord.ServiceDate = request.ServiceDate;
                isThereServiceRecordRecord.Description = request.Description;
                isThereServiceRecordRecord.LaborCost = request.LaborCost;
                isThereServiceRecordRecord.VehicleId = request.VehicleId;
                isThereServiceRecordRecord.ServiceItems = request.ServiceItems;
                isThereServiceRecordRecord.CreatedAt = request.CreatedAt;
                isThereServiceRecordRecord.UpdatedAt = request.UpdatedAt;
                isThereServiceRecordRecord.IsDeleted = request.IsDeleted;


                _serviceRecordRepository.Update(isThereServiceRecordRecord);
                await _serviceRecordRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

