
using Business.Handlers.Organizations.Commands;
using FluentValidation;

namespace Business.Handlers.Organizations.ValidationRules
{

    public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Employees).NotEmpty();
            RuleFor(x => x.Customers).NotEmpty();

        }
    }
    public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Employees).NotEmpty();
            RuleFor(x => x.Customers).NotEmpty();

        }
    }
}