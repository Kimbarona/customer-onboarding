using Domain;

namespace Application.Interfaces;

using Application.DTOs;

public interface ICustomerService
{
    Task<Customer> Create(CreateCustomerDto dto);
    Task<List<Customer>> GetAll();
    Task<Customer> GetById(Guid id);
}