
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
using Business.Handlers.People.ValidationRules;

namespace Business.Handlers.People.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePersonCommand : IRequest<IResult>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, IResult>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IMediator _mediator;
            public CreatePersonCommandHandler(IPersonRepository personRepository, IMediator mediator)
            {
                _personRepository = personRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePersonValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var isTherePersonRecord = _personRepository.Query().Any(u => u.FirstName == request.FirstName);

                if (isTherePersonRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPerson = new Person
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,

                };

                _personRepository.Add(addedPerson);
                await _personRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}