using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Commands.Update;

public class UpdatedBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Basket? Basket { get; set; }
    public Product? Product { get; set; }
}