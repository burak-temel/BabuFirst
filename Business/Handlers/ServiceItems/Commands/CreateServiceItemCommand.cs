
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
using Business.Handlers.ServiceItems.ValidationRules;

namespace Business.Handlers.ServiceItems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateServiceItemCommand : IRequest<IResult>
    {

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public class CreateServiceItemCommandHandler : IRequestHandler<CreateServiceItemCommand, IResult>
        {
            private readonly IServiceItemRepository _serviceItemRepository;
            private readonly IMediator _mediator;
            public CreateServiceItemCommandHandler(IServiceItemRepository serviceItemRepository, IMediator mediator)
            {
                _serviceItemRepository = serviceItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateServiceItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateServiceItemCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceItemRecord = _serviceItemRepository.Query().Any(u => u.ProductId == request.ProductId);

                if (isThereServiceItemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedServiceItem = new ServiceItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,

                };

                _serviceItemRepository.Add(addedServiceItem);
                await _serviceItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}