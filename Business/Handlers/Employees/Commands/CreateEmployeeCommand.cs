
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Employees.ValidationRules;
using System;

namespace Business.Handlers.Employees.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEmployeeCommand : IRequest<IResult>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int OrganizationId { get; set; }
        public decimal Salary { get; set; }


        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;
            public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEmployeeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var isThereEmployeeRecord = _employeeRepository.Query().Any(u => u.Salary == request.Salary);

                if (isThereEmployeeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEmployee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Salary = request.Salary,
                    OrganizationId = request.OrganizationId,
                    CreatedAt = DateTime.UtcNow

                };

                _employeeRepository.Add(addedEmployee);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}