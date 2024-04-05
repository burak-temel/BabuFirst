
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
using Business.Handlers.Organizations.ValidationRules;


namespace Business.Handlers.Organizations.Commands
{


    public class UpdateOrganizationCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public System.Collections.Generic.ICollection<Employee> Employees { get; set; }
        public System.Collections.Generic.ICollection<Customer> Customers { get; set; }

        public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, IResult>
        {
            private readonly IOrganizationRepository _organizationRepository;
            private readonly IMediator _mediator;

            public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMediator mediator)
            {
                _organizationRepository = organizationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrganizationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
            {
                var isThereOrganizationRecord = await _organizationRepository.GetAsync(u => u.Id == request.Id);


                isThereOrganizationRecord.Name = request.Name;
                isThereOrganizationRecord.Address = request.Address;
                //TODO

                _organizationRepository.Update(isThereOrganizationRecord);
                await _organizationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

