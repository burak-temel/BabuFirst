﻿
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
using Business.Handlers.Products.ValidationRules;
using Core.CrossCuttingConcerns.Context;
using Core.Extensions;
using System;

namespace Business.Handlers.Products.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TaxRateId { get; set; }


        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly BabuAppContext _context;
            public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator, IAppContextService appContextService)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _context = appContextService.GetAppContext();
            }

            [ValidationAspect(typeof(CreateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = _productRepository.Query().Any(u => u.Name == request.Name);

                if (isThereProductRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProduct = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    TaxRateId = request.TaxRateId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _context.UserId
                };

                _productRepository.Add(addedProduct);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}