
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
using Business.Handlers.Products.ValidationRules;


namespace Business.Handlers.Products.Commands
{


    public class UpdateProductCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TaxRateId { get; set; }
        public System.Collections.Generic.ICollection<ServiceItem> ServiceItems { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = await _productRepository.GetAsync(u => u.Id == request.Id);


                isThereProductRecord.Name = request.Name;
                isThereProductRecord.Price = request.Price;
                isThereProductRecord.TaxRateId = request.TaxRateId;
                isThereProductRecord.ServiceItems = request.ServiceItems;


                _productRepository.Update(isThereProductRecord);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

