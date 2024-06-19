using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BasketItems.Queries.GetList;

public class GetListBasketItemListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Basket? Basket { get; set; }
    public Product? Product { get; set; }
}