
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ServiceItems.Queries
{
    public class GetServiceItemQuery : IRequest<IDataResult<ServiceItem>>
    {
        public int ServiceRecordId { get; set; }

        public class GetServiceItemQueryHandler : IRequestHandler<GetServiceItemQuery, IDataResult<ServiceItem>>
        {
            private readonly IServiceItemRepository _serviceItemRepository;
            private readonly IMediator _mediator;

            public GetServiceItemQueryHandler(IServiceItemRepository serviceItemRepository, IMediator mediator)
            {
                _serviceItemRepository = serviceItemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ServiceItem>> Handle(GetServiceItemQuery request, CancellationToken cancellationToken)
            {
                var serviceItem = await _serviceItemRepository.GetAsync(p => p.ServiceRecordId == request.ServiceRecordId);
                return new SuccessDataResult<ServiceItem>(serviceItem);
            }
        }
    }
}
