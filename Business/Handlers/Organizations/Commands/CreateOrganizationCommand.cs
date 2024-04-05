
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
using Business.Handlers.Organizations.ValidationRules;
using Core.Entities.Concrete;

namespace Business.Handlers.Organizations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrganizationCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public System.Collections.Generic.ICollection<Employee> Employees { get; set; }
        public System.Collections.Generic.ICollection<Customer> Customers { get; set; }


        public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, IResult>
        {
            private readonly IOrganizationRepository _organizationRepository;
            private readonly IMediator _mediator;
            public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMediator mediator)
            {
                _organizationRepository = organizationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrganizationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
            {
                var isThereOrganizationRecord = _organizationRepository.Query().Any(u => u.Name == request.Name);

                if (isThereOrganizationRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrganization = new Organization
                {
                    Name = request.Name,
                    Address = request.Address,
                    //Employees = request.Employees,
                    //Customers = request.Customers,

                };

                _organizationRepository.Add(addedOrganization);
                await _organizationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}