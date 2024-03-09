
using Business.Handlers.Employees.Commands;
using FluentValidation;

namespace Business.Handlers.Employees.ValidationRules
{

    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Salary).NotEmpty();

        }
    }
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Salary).NotEmpty();

        }
    }
}