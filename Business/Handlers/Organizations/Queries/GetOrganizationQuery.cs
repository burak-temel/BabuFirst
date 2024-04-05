
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;

namespace Business.Handlers.Organizations.Queries
{
    public class GetOrganizationQuery : IRequest<IDataResult<Organization>>
    {
        public int Id { get; set; }

        public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, IDataResult<Organization>>
        {
            private readonly IOrganizationRepository _organizationRepository;
            private readonly IMediator _mediator;

            public GetOrganizationQueryHandler(IOrganizationRepository organizationRepository, IMediator mediator)
            {
                _organizationRepository = organizationRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Organization>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
            {
                var organization = await _organizationRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Organization>(organization);
            }
        }
    }
}
