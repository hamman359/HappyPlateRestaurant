using HappyPlate.Application.Products.AddProduct;
using HappyPlate.Presentation.Abstractions;
using HappyPlate.Presentation.Contracts.Product;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.Presentation.Controllers;

[Route("api/products")]
public sealed class ProductController : ApiController
{
    public ProductController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(
        Guid id,
        CancellationToken cancelationToken)
    {
        //TODO: Implement
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(
        [FromBody] AddProductRequest request,
        CancellationToken cancelationToken)
    {
        var command = new AddProductCommand(
            request.name,
            request.description,
            request.price,
            request.category);

        var result = await Sender.Send(command, cancelationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = result.Value },
            result.Value);

    }
}
