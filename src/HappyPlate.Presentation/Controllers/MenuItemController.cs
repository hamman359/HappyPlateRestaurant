using HappyPlate.Application.MenuItems.AddMenuItem;
using HappyPlate.Application.MenuItems.GetMenuItemById;
using HappyPlate.Domain.Shared;
using HappyPlate.Presentation.Abstractions;
using HappyPlate.Presentation.Contracts.MenuItems;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.Presentation.Controllers;

[Route("api/menuitems")]
public sealed class MenuItemController : ApiController
{
    public MenuItemController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMenuItemById(
        Guid id,
        CancellationToken cancelationToken)
    {
        var query = new GetMenuItemByIdQuery(id);

        Result<MenuItemResponse> response = await Sender.Send(query, cancelationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> AddMenuItem(
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
            nameof(GetMenuItemById),
            new { id = result.Value },
            result.Value);

    }
}
