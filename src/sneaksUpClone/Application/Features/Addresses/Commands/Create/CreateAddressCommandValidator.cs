using FluentValidation;

namespace Application.Features.Addresses.Commands.Create;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.CreateAddressDto.AddressName).NotEmpty();
        RuleFor(c => c.CreateAddressDto.City).NotEmpty();
        RuleFor(c => c.CreateAddressDto.Country).NotEmpty();
        RuleFor(c => c.CreateAddressDto.ZipCode).NotEmpty();
        RuleFor(c => c.CreateAddressDto.ContactName).NotEmpty();
    }
}