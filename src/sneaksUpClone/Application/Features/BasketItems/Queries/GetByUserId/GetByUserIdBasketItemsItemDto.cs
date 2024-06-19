using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Queries.GetByUserId;
public class GetByUserIdBasketItemsItemDto
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Guid BasketUserId { get; set; }
    public double BasketTotalPrice { get; set; }
    public string ProductName { get; set; }
    public int ProductStockAmount { get; set; }
    public double ProductPrice { get; set; }

    public GetByUserIdBasketItemsItemDto()
    {
        ProductName = string.Empty;
    }

    public GetByUserIdBasketItemsItemDto(Guid id, Guid basketId, Guid productId, Guid basketUserId, double basketTotalPrice, string productName, int productStockAmount, double productPrice)
    {
        Id = id;
        BasketId = basketId;
        ProductId = productId;
        BasketUserId = basketUserId;
        BasketTotalPrice = basketTotalPrice;
        ProductName = productName;
        ProductStockAmount = productStockAmount;
        ProductPrice = productPrice;
    }
}
