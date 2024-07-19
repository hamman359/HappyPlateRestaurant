using HappyPlate.Application.Products.AddProduct;
using HappyPlate.Application.Products.GetProductById;
using HappyPlate.Domain.Shared;
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
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);


        Result<ProductResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
        //806990ea-ba86-4fa3-bf22-edbd95a68a33
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
