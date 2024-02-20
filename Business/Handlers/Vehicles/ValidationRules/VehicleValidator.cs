
using Business.Handlers.Vehicles.Commands;
using FluentValidation;

namespace Business.Handlers.Vehicles.ValidationRules
{

    public class CreateVehicleValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleValidator()
        {
            RuleFor(x => x.LicensePlate).NotEmpty();
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.VIN).NotEmpty();
            RuleFor(x => x.Mileage).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ServiceRecords).NotEmpty();

        }
    }
    public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleValidator()
        {
            RuleFor(x => x.LicensePlate).NotEmpty();
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.VIN).NotEmpty();
            RuleFor(x => x.Mileage).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ServiceRecords).NotEmpty();

        }
    }
}