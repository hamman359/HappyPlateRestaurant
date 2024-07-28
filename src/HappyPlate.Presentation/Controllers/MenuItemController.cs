using HappyPlate.Application.MenuItems;
using HappyPlate.Application.MenuItems.AddMenuItem;
using HappyPlate.Application.MenuItems.ChangeMenuItemPrice;
using HappyPlate.Application.MenuItems.DeleteMenuItem;
using HappyPlate.Application.MenuItems.GetMenuItemById;
using HappyPlate.Application.MenuItems.SetMenuItemAvailable;
using HappyPlate.Application.MenuItems.SetMenuItemUnavailable;
using HappyPlate.Domain.Shared;
using HappyPlate.Presentation.Abstractions;
using HappyPlate.Presentation.Contracts.MenuItems;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class MenuItemController : ApiController
{
    public MenuItemController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancelationToken)
    {
        var query = new GetMenuItemByIdQuery(id);

        Result<MenuItemResponse> response = await Sender.Send(query, cancelationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(
        [FromBody] AddMenuItemRequest request,
        CancellationToken cancelationToken)
    {
        var command = new AddMenuItemCommand(
            request.name,
            request.description,
            request.price,
            request.category,
            request.image,
            request.isAvailable);

        var result = await Sender.Send(command, cancelationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }

    [HttpPut("{id:guid}/SetAsUnavailable")]
    public async Task<IActionResult> SetAsUnavailable(
        Guid id,
        CancellationToken cancelationToken)
    {
        var command = new SetMenuItemUnavailableCommand(id);

        Result<bool> response = await Sender.Send(command, cancelationToken);

        return response.IsSuccess
            ? Ok()
            : NotFound(response.Error);
    }

    [HttpPut("{id:guid}/SetAsAvailable")]
    public async Task<IActionResult> SetAsAvailable(
    Guid id,
    CancellationToken cancelationToken)
    {
        var command = new SetMenuItemAvailableCommand(id);

        Result<bool> response = await Sender.Send(command, cancelationToken);

        return response.IsSuccess
            ? Ok()
            : NotFound(response.Error);
    }

    [HttpPost("{id:guid}/Delete")]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancelationToken)
    {
        var command = new DeleteMenuItemCommand(id);

        Result<bool> response = await Sender.Send(command, cancelationToken);

        return response.IsSuccess
            ? Ok()
            : NotFound(response.Error);
    }

    [HttpPut("{id:guid}/ChangePrice")]
    public async Task<IActionResult> ChangePrice(
    Guid id,
    [FromBody] float price,
    CancellationToken cancelationToken)
    {
        var command = new ChangeMenuItemPriceCommand(
            id,
            price);

        Result<Guid> result = await Sender.Send(command, cancelationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok();
    }
}
