using Application.Features.BasketItems.Commands.Create;
using Application.Features.BasketItems.Commands.Delete;
using Application.Features.BasketItems.Commands.Update;
using Application.Features.BasketItems.Queries.GetById;
using Application.Features.BasketItems.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.BasketItems.Queries.GetByUserId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Guid productId)
    {
        CreateBasketItemCommand command = new CreateBasketItemCommand { UserId = getUserIdFromRequest(), ProductId = productId };
        CreatedBasketItemResponse response = await Mediator.Send(command);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBasketItemCommand updateBasketItemCommand)
    {
        UpdatedBasketItemResponse response = await Mediator.Send(updateBasketItemCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBasketItemResponse response = await Mediator.Send(new DeleteBasketItemCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBasketItemResponse response = await Mediator.Send(new GetByIdBasketItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBasketItemQuery getListBasketItemQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBasketItemListItemDto> response = await Mediator.Send(getListBasketItemQuery);
        return Ok(response);
    }

    [HttpGet("GetByUserIdBasketItem")]
    public async Task<IActionResult> GetByUserIdBasketItem()
    {
        GetByUserIdBasketItemQuery query = new() { UserId = getUserIdFromRequest() };
        GetListResponse<GetByUserIdBasketItemsItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}