
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


namespace Business.Handlers.Organizations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOrganizationCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, IResult>
        {
            private readonly IOrganizationRepository _organizationRepository;
            private readonly IMediator _mediator;

            public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMediator mediator)
            {
                _organizationRepository = organizationRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
            {
                var organizationToDelete = _organizationRepository.Get(p => p.Id == request.Id);

                _organizationRepository.Delete(organizationToDelete);
                await _organizationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

