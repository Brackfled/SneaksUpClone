using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StockAmount { get; set; }
    public double Price { get; set; }
}