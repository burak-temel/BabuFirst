
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
using System;
using Core.Extensions;
using Core.CrossCuttingConcerns.Context;

namespace Business.Handlers.Products.Commands
{


    public class UpdateProductCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TaxRateId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly BabuAppContext _context;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator, IAppContextService appContextService)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _context = appContextService.GetAppContext();
            }

            [ValidationAspect(typeof(UpdateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productRecord = await _productRepository.GetAsync(u => u.Id == request.Id && u.OrganizationId == _context.OrganizationId);

                if (productRecord == null)
                {
                    return new ErrorResult(Messages.ProductNotFound);
                }

                productRecord.Name = request.Name;
                productRecord.Price = request.Price;
                productRecord.TaxRateId = request.TaxRateId;
                productRecord.UpdatedBy = _context.UserId;
                productRecord.OrganizationId = _context.OrganizationId;
                productRecord.UpdatedAt = DateTime.UtcNow;

                _productRepository.Update(productRecord);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

