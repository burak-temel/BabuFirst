
using Business.Handlers.Invoices.Commands;
using FluentValidation;

namespace Business.Handlers.Invoices.ValidationRules
{

    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.InvoiceDate).NotEmpty();
            RuleFor(x => x.ServiceRecords).NotEmpty();
            RuleFor(x => x.TotalAmount).NotEmpty();

        }
    }
    public class UpdateInvoiceValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.InvoiceDate).NotEmpty();
            RuleFor(x => x.ServiceRecords).NotEmpty();
            RuleFor(x => x.TotalAmount).NotEmpty();

        }
    }
}