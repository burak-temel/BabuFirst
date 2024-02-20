
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


namespace Business.Handlers.TaxRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteTaxRateCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteTaxRateCommandHandler : IRequestHandler<DeleteTaxRateCommand, IResult>
        {
            private readonly ITaxRateRepository _taxRateRepository;
            private readonly IMediator _mediator;

            public DeleteTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IMediator mediator)
            {
                _taxRateRepository = taxRateRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteTaxRateCommand request, CancellationToken cancellationToken)
            {
                var taxRateToDelete = _taxRateRepository.Get(p => p.Id == request.Id);

                _taxRateRepository.Delete(taxRateToDelete);
                await _taxRateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

