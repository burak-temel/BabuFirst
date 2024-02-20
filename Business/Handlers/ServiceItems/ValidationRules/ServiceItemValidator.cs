
using Business.Handlers.ServiceItems.Commands;
using FluentValidation;

namespace Business.Handlers.ServiceItems.ValidationRules
{

    public class CreateServiceItemValidator : AbstractValidator<CreateServiceItemCommand>
    {
        public CreateServiceItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
    public class UpdateServiceItemValidator : AbstractValidator<UpdateServiceItemCommand>
    {
        public UpdateServiceItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();

        }
    }
}