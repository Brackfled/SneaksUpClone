using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.Update;

public class UpdatedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StockAmount { get; set; }
    public double Price { get; set; }
}