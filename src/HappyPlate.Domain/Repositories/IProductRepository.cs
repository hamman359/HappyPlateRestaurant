using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;
public interface IProductRepository
{
    void Add(Product product);
}
