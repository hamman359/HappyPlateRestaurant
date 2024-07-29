using HappyPlate.Application.MenuItems.Queries.GetAllMenuItems;
using HappyPlate.Application.MenuItems.Queries.GetMenuItemsByCategory;
using HappyPlate.Presentation.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class MenuItemsController : ApiController
{
    public MenuItemsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken canellationToken)
    {
        var query = new GetAllMenuItemsQuery();

        var response = await Sender.Send(query, canellationToken);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }

    [HttpGet("{category:string}")]
    public async Task<IActionResult> GetByCategory(
        string category,
        CancellationToken canellationToken)
    {
        var query = new GetMenuItemsByCategoryQuery(category);

        var response = await Sender.Send(query, canellationToken);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }
}
