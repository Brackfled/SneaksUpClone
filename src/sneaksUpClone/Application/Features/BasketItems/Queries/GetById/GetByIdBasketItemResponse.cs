using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BasketItems.Queries.GetById;

public class GetByIdBasketItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Basket? Basket { get; set; }
    public Product? Product { get; set; }
}