
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Employees.ValidationRules;


namespace Business.Handlers.Employees.Commands
{


    public class UpdateEmployeeCommand : IRequest<IResult>
    {
        public int OrganizationId { get; set; }
        public decimal Salary { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;

            public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEmployeeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeRecord = await _employeeRepository.GetAsync(u => u.OrganizationId == request.OrganizationId);


                isThereEmployeeRecord.Salary = request.Salary;


                _employeeRepository.Update(isThereEmployeeRecord);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

