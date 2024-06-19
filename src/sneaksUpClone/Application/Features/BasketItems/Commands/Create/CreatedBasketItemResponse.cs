using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Commands.Create;

public class CreatedBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
}