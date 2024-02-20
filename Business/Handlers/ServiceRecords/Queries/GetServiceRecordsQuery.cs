
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

namespace Business.Handlers.ServiceRecords.Queries
{

    public class GetServiceRecordsQuery : IRequest<IDataResult<IEnumerable<ServiceRecord>>>
    {
        public class GetServiceRecordsQueryHandler : IRequestHandler<GetServiceRecordsQuery, IDataResult<IEnumerable<ServiceRecord>>>
        {
            private readonly IServiceRecordRepository _serviceRecordRepository;
            private readonly IMediator _mediator;

            public GetServiceRecordsQueryHandler(IServiceRecordRepository serviceRecordRepository, IMediator mediator)
            {
                _serviceRecordRepository = serviceRecordRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ServiceRecord>>> Handle(GetServiceRecordsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ServiceRecord>>(await _serviceRecordRepository.GetListAsync());
            }
        }
    }
}