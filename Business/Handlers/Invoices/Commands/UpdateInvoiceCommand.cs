
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
using Business.Handlers.Invoices.ValidationRules;


namespace Business.Handlers.Invoices.Commands
{


    public class UpdateInvoiceCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public System.Collections.Generic.ICollection<ServiceRecord> ServiceRecords { get; set; }
        public decimal TotalAmount { get; set; }

        public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, IResult>
        {
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IMediator _mediator;

            public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
            {
                _invoiceRepository = invoiceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateInvoiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
            {
                var isThereInvoiceRecord = await _invoiceRepository.GetAsync(u => u.Id == request.Id);


                isThereInvoiceRecord.CustomerId = request.CustomerId;
                isThereInvoiceRecord.InvoiceDate = request.InvoiceDate;
                isThereInvoiceRecord.ServiceRecords = request.ServiceRecords;
                isThereInvoiceRecord.TotalAmount = request.TotalAmount;


                _invoiceRepository.Update(isThereInvoiceRecord);
                await _invoiceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

