
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Context;

namespace Business.Handlers.Employees.Queries
{
    public class GetEmployeeQuery : IRequest<IDataResult<Employee>>
    {
        public int EmployeeId { get; set; }

        public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, IDataResult<Employee>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;
            private readonly Core.Extensions.AppContext _appContext;

            public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMediator mediator, IAppContextService appContextService)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
                _appContext = appContextService.GetAppContext();
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetAsync(p => p.Id == request.EmployeeId && p.OrganizationId == _appContext.OrganizationId);
                return new SuccessDataResult<Employee>(employee);
            }
        }
    }
}
