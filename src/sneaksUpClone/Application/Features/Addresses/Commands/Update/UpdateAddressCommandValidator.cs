using FluentValidation;

namespace Application.Features.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.AddressName).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.ZipCode).NotEmpty();
        RuleFor(c => c.ContactName).NotEmpty();
        RuleFor(c => c.User).NotEmpty();
    }
}