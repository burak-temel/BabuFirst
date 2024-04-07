
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
using System;
using Core.CrossCuttingConcerns.Context;

namespace Business.Handlers.Employees.Commands
{


    public class UpdateEmployeeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Salary { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IResult>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMediator _mediator;
            private readonly Core.Extensions.AppContext _appContext;

            public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator,IAppContextService appContextService)
            {
                _employeeRepository = employeeRepository;
                _mediator = mediator;
                _appContext = appContextService.GetAppContext();
            }

            [ValidationAspect(typeof(UpdateEmployeeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employeeRecord = await _employeeRepository.GetAsync(u => u.Id == request.Id);

                if (employeeRecord == null)
                {
                    return new ErrorResult(Messages.UserNotFound);
                }

                if (employeeRecord.PhoneNumber != request.PhoneNumber)
                {
                    var isPhoneNumberExist = _employeeRepository.Query().Any(u => u.PhoneNumber == request.PhoneNumber);

                    if (isPhoneNumberExist == true)
                        return new ErrorResult(Messages.NameAlreadyExist);
                    employeeRecord.PhoneNumber = request.PhoneNumber;
                }


                employeeRecord.Salary = request.Salary;
                employeeRecord.FirstName = request.FirstName;
                employeeRecord.LastName = request.LastName;
                employeeRecord.Email = request.Email;
                employeeRecord.UpdatedAt = DateTime.UtcNow;
                employeeRecord.UpdatedBy = _appContext.UserId;

                _employeeRepository.Update(employeeRecord);
                await _employeeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

