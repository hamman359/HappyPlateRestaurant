using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;

public interface ICustomerRepository
{
    void Add(Customer customer);
}
