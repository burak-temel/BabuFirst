
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
using Business.Handlers.ServiceItems.ValidationRules;


namespace Business.Handlers.ServiceItems.Commands
{


    public class UpdateServiceItemCommand : IRequest<IResult>
    {
        public int ServiceRecordId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public class UpdateServiceItemCommandHandler : IRequestHandler<UpdateServiceItemCommand, IResult>
        {
            private readonly IServiceItemRepository _serviceItemRepository;
            private readonly IMediator _mediator;

            public UpdateServiceItemCommandHandler(IServiceItemRepository serviceItemRepository, IMediator mediator)
            {
                _serviceItemRepository = serviceItemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateServiceItemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateServiceItemCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceItemRecord = await _serviceItemRepository.GetAsync(u => u.ServiceRecordId == request.ServiceRecordId);


                isThereServiceItemRecord.ProductId = request.ProductId;
                isThereServiceItemRecord.Quantity = request.Quantity;
                isThereServiceItemRecord.UnitPrice = request.UnitPrice;


                _serviceItemRepository.Update(isThereServiceItemRecord);
                await _serviceItemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

