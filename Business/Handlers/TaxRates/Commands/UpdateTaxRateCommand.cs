
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
using Business.Handlers.TaxRates.ValidationRules;
using System;

namespace Business.Handlers.TaxRates.Commands
{


    public class UpdateTaxRateCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateTaxRateCommandHandler : IRequestHandler<UpdateTaxRateCommand, IResult>
        {
            private readonly ITaxRateRepository _taxRateRepository;
            private readonly IMediator _mediator;

            public UpdateTaxRateCommandHandler(ITaxRateRepository taxRateRepository, IMediator mediator)
            {
                _taxRateRepository = taxRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateTaxRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateTaxRateCommand request, CancellationToken cancellationToken)
            {
                var taxRateRecord = await _taxRateRepository.GetAsync(u => u.Id == request.Id);

                taxRateRecord.Description = request.Description;
                taxRateRecord.Rate = request.Rate;
                taxRateRecord.UpdatedAt = DateTime.UtcNow;
                //taxRateRecord.UpdatedBy = request.Updated; TODO

                _taxRateRepository.Update(taxRateRecord);
                await _taxRateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

