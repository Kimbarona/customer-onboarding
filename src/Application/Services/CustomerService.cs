using Application.DTOs;
using Application.Interfaces;
using Domain;
using System.Text.RegularExpressions;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Customer> Create(CreateCustomerDto dto)
    {
        Validate(dto);

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = Regex.Replace(dto.PhoneNumber, @"\D", ""),
            Signature = dto.Signature,
            DateCreated = DateTime.UtcNow
        };

        return await _repo.Add(customer);
    }

    public async Task<List<Customer>> GetAll() => await _repo.GetAll();

    public async Task<Customer> GetById(Guid id)
    {
        var customer = await _repo.GetById(id);
        if (customer == null)
            throw new KeyNotFoundException("Customer not found");
        return customer;
    }

    private void Validate(CreateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            throw new ArgumentException("First Name is required");

        if (string.IsNullOrWhiteSpace(dto.LastName))
            throw new ArgumentException("Last Name is required");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required");

        if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format");

        var cleanPhone = Regex.Replace(dto.PhoneNumber ?? "", @"\D", "");

        if (!Regex.IsMatch(cleanPhone, @"^09\d{9}$"))
            throw new ArgumentException("Invalid phone number");

        if (string.IsNullOrWhiteSpace(dto.Signature))
            throw new ArgumentException("Signature is required");
    }
}