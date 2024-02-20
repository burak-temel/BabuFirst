
using Business.Handlers.ServiceRecords.Commands;
using FluentValidation;

namespace Business.Handlers.ServiceRecords.ValidationRules
{

    public class CreateServiceRecordValidator : AbstractValidator<CreateServiceRecordCommand>
    {
        public CreateServiceRecordValidator()
        {
            RuleFor(x => x.ServiceDate).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.LaborCost).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.ServiceItems).NotEmpty();
            RuleFor(x => x.CreatedAt).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEmpty();

        }
    }
    public class UpdateServiceRecordValidator : AbstractValidator<UpdateServiceRecordCommand>
    {
        public UpdateServiceRecordValidator()
        {
            RuleFor(x => x.ServiceDate).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.LaborCost).NotEmpty();
            RuleFor(x => x.VehicleId).NotEmpty();
            RuleFor(x => x.ServiceItems).NotEmpty();
            RuleFor(x => x.CreatedAt).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEmpty();

        }
    }
}