using FluentValidation;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommandValidator : AbstractValidator<CreateBasketItemCommand>
{
    public CreateBasketItemCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}