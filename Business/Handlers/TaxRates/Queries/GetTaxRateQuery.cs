
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.TaxRates.Queries
{
    public class GetTaxRateQuery : IRequest<IDataResult<TaxRate>>
    {
        public int Id { get; set; }

        public class GetTaxRateQueryHandler : IRequestHandler<GetTaxRateQuery, IDataResult<TaxRate>>
        {
            private readonly ITaxRateRepository _taxRateRepository;
            private readonly IMediator _mediator;

            public GetTaxRateQueryHandler(ITaxRateRepository taxRateRepository, IMediator mediator)
            {
                _taxRateRepository = taxRateRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<TaxRate>> Handle(GetTaxRateQuery request, CancellationToken cancellationToken)
            {
                var taxRate = await _taxRateRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<TaxRate>(taxRate);
            }
        }
    }
}
