
using Business.Handlers.Customers.Commands;
using FluentValidation;

namespace Business.Handlers.Customers.ValidationRules
{

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            //todo write rule 
            //RuleFor(x => x.Vehicles).NotEmpty();
            //RuleFor(x => x.Invoices).NotEmpty();

        }
    }
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Vehicles).NotEmpty();
            RuleFor(x => x.Invoices).NotEmpty();

        }
    }
}