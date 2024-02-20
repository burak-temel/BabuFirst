
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
using Business.Handlers.TaxRates.ValidationRules;

namespace Business.Handlers.TaxRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTaxRateCommand : IRequest<IResult>
    {

        public string Description { get; set; }
        public decimal Rate { get; set; }
        public System.Collections.Generic.ICollection<Product> Products { get; set; }


        public class CreateTaxRateCommandHandler : IRequestHandler<CreateTaxRateCommand, IResult>
        {
            private readonly ITaxRateRepository _taxRateRepository;
            private readonly IMediator _mediator;
            public CreateTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IMediator mediator)
            {
                _taxRateRepository = taxRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateTaxRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateTaxRateCommand request, CancellationToken cancellationToken)
            {
                var isThereTaxRateRecord = _taxRateRepository.Query().Any(u => u.Description == request.Description);

                if (isThereTaxRateRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedTaxRate = new TaxRate
                {
                    Description = request.Description,
                    Rate = request.Rate,
                    Products = request.Products,

                };

                _taxRateRepository.Add(addedTaxRate);
                await _taxRateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}