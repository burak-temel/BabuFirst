
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


namespace Business.Handlers.ServiceItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteServiceItemCommand : IRequest<IResult>
    {
        public int ServiceRecordId { get; set; }

        public class DeleteServiceItemCommandHandler : IRequestHandler<DeleteServiceItemCommand, IResult>
        {
            private readonly IServiceItemRepository _serviceItemRepository;
            private readonly IMediator _mediator;

            public DeleteServiceItemCommandHandler(IServiceItemRepository serviceItemRepository, IMediator mediator)
            {
                _serviceItemRepository = serviceItemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteServiceItemCommand request, CancellationToken cancellationToken)
            {
                var serviceItemToDelete = _serviceItemRepository.Get(p => p.ServiceRecordId == request.ServiceRecordId);

                _serviceItemRepository.Delete(serviceItemToDelete);
                await _serviceItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

