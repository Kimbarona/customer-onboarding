using Domain;

namespace Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> Add(Customer customer);
    Task<List<Customer>> GetAll();
    Task<Customer> GetById(Guid id);
}