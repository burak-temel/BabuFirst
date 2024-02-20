
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

namespace Business.Handlers.Organizations.Queries
{

    public class GetOrganizationsQuery : IRequest<IDataResult<IEnumerable<Organization>>>
    {
        public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IDataResult<IEnumerable<Organization>>>
        {
            private readonly IOrganizationRepository _organizationRepository;
            private readonly IMediator _mediator;

            public GetOrganizationsQueryHandler(IOrganizationRepository organizationRepository, IMediator mediator)
            {
                _organizationRepository = organizationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Organization>>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Organization>>(await _organizationRepository.GetListAsync());
            }
        }
    }
}