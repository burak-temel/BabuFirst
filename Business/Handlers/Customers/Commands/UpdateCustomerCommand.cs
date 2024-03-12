
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
using Business.Handlers.Customers.ValidationRules;
using DataAccess.Concrete.EntityFramework;


namespace Business.Handlers.Customers.Commands
{


    public class UpdateCustomerCommand : IRequest<IResult>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get; set; }


        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCustomerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customerRecord = await _customerRepository.GetAsync(u => u.Id == request.Id);

                if (customerRecord == null)
                {
                    return new ErrorResult(Messages.UserNotFound);
                }

                if (customerRecord.PhoneNumber != request.PhoneNumber)
                {
                    var isPhoneNumberExist = _customerRepository.Query().Any(u => u.PhoneNumber == request.PhoneNumber);

                    if (isPhoneNumberExist == true)
                        return new ErrorResult(Messages.NameAlreadyExist);
                    customerRecord.PhoneNumber = request.PhoneNumber;
                }


                customerRecord.FirstName = request.FirstName;
                customerRecord.LastName = request.LastName;
                customerRecord.Email = request.Email;

                _customerRepository.Update(customerRecord);
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

