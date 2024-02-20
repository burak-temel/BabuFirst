
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ServiceRecords.Queries
{
    public class GetServiceRecordQuery : IRequest<IDataResult<ServiceRecord>>
    {
        public int Id { get; set; }

        public class GetServiceRecordQueryHandler : IRequestHandler<GetServiceRecordQuery, IDataResult<ServiceRecord>>
        {
            private readonly IServiceRecordRepository _serviceRecordRepository;
            private readonly IMediator _mediator;

            public GetServiceRecordQueryHandler(IServiceRecordRepository serviceRecordRepository, IMediator mediator)
            {
                _serviceRecordRepository = serviceRecordRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ServiceRecord>> Handle(GetServiceRecordQuery request, CancellationToken cancellationToken)
            {
                var serviceRecord = await _serviceRecordRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ServiceRecord>(serviceRecord);
            }
        }
    }
}
