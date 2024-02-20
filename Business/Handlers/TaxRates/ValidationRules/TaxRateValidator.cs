
using Business.Handlers.TaxRates.Commands;
using FluentValidation;

namespace Business.Handlers.TaxRates.ValidationRules
{

    public class CreateTaxRateValidator : AbstractValidator<CreateTaxRateCommand>
    {
        public CreateTaxRateValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Rate).NotEmpty();
            RuleFor(x => x.Products).NotEmpty();

        }
    }
    public class UpdateTaxRateValidator : AbstractValidator<UpdateTaxRateCommand>
    {
        public UpdateTaxRateValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Rate).NotEmpty();
            RuleFor(x => x.Products).NotEmpty();

        }
    }
}