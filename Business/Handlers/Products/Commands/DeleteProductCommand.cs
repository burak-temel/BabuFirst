
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Core.CrossCuttingConcerns.Context;

namespace Business.Handlers.Products.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProductCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly BabuAppContext _context;

            public DeleteProductCommandHandler(IProductRepository productRepository, IMediator mediator, IAppContextService appContextService)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _context = appContextService.GetAppContext();
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var productToDelete = await _productRepository.GetAsync(p => p.Id == request.Id && p.OrganizationId == _context.OrganizationId);

                if (productToDelete == null)
                {
                    return new ErrorResult(Messages.ProductNotFound);
                }

                _productRepository.Delete(productToDelete);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

