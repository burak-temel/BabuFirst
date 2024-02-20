
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

namespace Business.Handlers.ServiceItems.Queries
{

    public class GetServiceItemsQuery : IRequest<IDataResult<IEnumerable<ServiceItem>>>
    {
        public class GetServiceItemsQueryHandler : IRequestHandler<GetServiceItemsQuery, IDataResult<IEnumerable<ServiceItem>>>
        {
            private readonly IServiceItemRepository _serviceItemRepository;
            private readonly IMediator _mediator;

            public GetServiceItemsQueryHandler(IServiceItemRepository serviceItemRepository, IMediator mediator)
            {
                _serviceItemRepository = serviceItemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ServiceItem>>> Handle(GetServiceItemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ServiceItem>>(await _serviceItemRepository.GetListAsync());
            }
        }
    }
}