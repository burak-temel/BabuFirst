
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


namespace Business.Handlers.ServiceRecords.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteServiceRecordCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteServiceRecordCommandHandler : IRequestHandler<DeleteServiceRecordCommand, IResult>
        {
            private readonly IServiceRecordRepository _serviceRecordRepository;
            private readonly IMediator _mediator;

            public DeleteServiceRecordCommandHandler(IServiceRecordRepository serviceRecordRepository, IMediator mediator)
            {
                _serviceRecordRepository = serviceRecordRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteServiceRecordCommand request, CancellationToken cancellationToken)
            {
                var serviceRecordToDelete = _serviceRecordRepository.Get(p => p.Id == request.Id);

                _serviceRecordRepository.Delete(serviceRecordToDelete);
                await _serviceRecordRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

