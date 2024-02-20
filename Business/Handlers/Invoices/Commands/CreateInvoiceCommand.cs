
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
using Business.Handlers.Invoices.ValidationRules;

namespace Business.Handlers.Invoices.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateInvoiceCommand : IRequest<IResult>
    {

        public int CustomerId { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public System.Collections.Generic.ICollection<ServiceRecord> ServiceRecords { get; set; }
        public decimal TotalAmount { get; set; }


        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, IResult>
        {
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IMediator _mediator;
            public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMediator mediator)
            {
                _invoiceRepository = invoiceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateInvoiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
            {
                var isThereInvoiceRecord = _invoiceRepository.Query().Any(u => u.CustomerId == request.CustomerId);

                if (isThereInvoiceRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedInvoice = new Invoice
                {
                    CustomerId = request.CustomerId,
                    InvoiceDate = request.InvoiceDate,
                    ServiceRecords = request.ServiceRecords,
                    TotalAmount = request.TotalAmount,

                };

                _invoiceRepository.Add(addedInvoice);
                await _invoiceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}