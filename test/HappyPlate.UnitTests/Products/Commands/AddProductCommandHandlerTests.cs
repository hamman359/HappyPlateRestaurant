using FluentAssertions;

using HappyPlate.Application.Products.AddProduct;
using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;

using Moq;

namespace HappyPlate.UnitTests.Products.Commands;
public class AddProductCommandHandlerTests
{
    readonly Mock<IProductRepository> _productRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddProductCommandHandlerTests()
    {
        _productRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPriceIsNegative()
    {
        var command = new AddProductCommand("Product", "Description", -1.0f, "Category");

        var handler = new AddProductCommandHandler(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Price.Negative);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenPriceIsValid()
    {
        var command = new AddProductCommand("Product", "Description", 1.0f, "Category");

        var handler = new AddProductCommandHandler(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        _productRepositoryMock.Verify(
            x => x.Add(
                It.Is<Product>(p => p.Id == result.Value)),
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenPriceIsNegative()
    {
        var command = new AddProductCommand("Product", "Description", -1.0f, "Category");

        var handler = new AddProductCommandHandler(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
