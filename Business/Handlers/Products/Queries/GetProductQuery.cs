
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.CrossCuttingConcerns.Context;
using Business.Constants;

namespace Business.Handlers.Products.Queries
{
    public class GetProductQuery : IRequest<IDataResult<Product>>
    {
        public int Id { get; set; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IDataResult<Product>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly BabuAppContext _context;

            public GetProductQueryHandler(IProductRepository productRepository, IMediator mediator, IAppContextService appContextService)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _context = appContextService.GetAppContext();
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(p => p.Id == request.Id && p.OrganizationId == _context.OrganizationId);

                if (product == null)
                {
                    return new ErrorDataResult<Product>(Messages.ProductNotFound);
                }

                return new SuccessDataResult<Product>(product);
            }
        }
    }
}
