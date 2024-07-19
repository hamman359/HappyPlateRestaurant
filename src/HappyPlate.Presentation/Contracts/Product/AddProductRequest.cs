namespace HappyPlate.Presentation.Contracts.Product;

public sealed record AddProductRequest(
    string name,
    string description,
    float price,
    string category);
