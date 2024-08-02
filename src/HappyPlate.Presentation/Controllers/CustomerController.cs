using HappyPlate.Application.Customers.Commands.AddCustomer;
using HappyPlate.Application.Customers.Queries;
using HappyPlate.Presentation.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class CustomerController : ApiController
{
    public CustomerController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await Sender.Send(new GetCustomerByIdQuery(id), cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(
        [FromBody] AddCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }
}
