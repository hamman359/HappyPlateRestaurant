using HappyPlate.Application.Customers.Commands.AddCustomer;
using HappyPlate.Presentation.Abstractions;
using HappyPlate.Presentation.Contracts.Customers;

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
        return Ok();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(
        [FromBody] AddCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddCustomerCommand(
            request.firstName,
            request.lastName,
            request.email,
            request.areaCode,
            request.prefix,
            request.lineNumber,
            request.extension,
            request.addresses
                .Select(x => new AddressDto(
                    x.street,
                    x.city,
                    x.state,
                    x.zipCode,
                    x.country,
                    x.addressType))
                .ToList());

        var result = await Sender.Send(command, cancellationToken);

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
