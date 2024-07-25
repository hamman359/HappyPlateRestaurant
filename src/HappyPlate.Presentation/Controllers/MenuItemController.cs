using HappyPlate.Application.MenuItems.AddMenuItem;
using HappyPlate.Application.MenuItems.DeleteMenuItem;
using HappyPlate.Application.MenuItems.GetMenuItemById;
using HappyPlate.Application.MenuItems.ToggleMenuItemAvailability;
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

    [HttpPost("{id:guid}/ToggleAvailability")]
    public async Task<IActionResult> ToggleAvailability(
        Guid id,
        CancellationToken cancelationToken)
    {
        var command = new ToggleMenuItemAvailabilityCommand(id);

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
}
