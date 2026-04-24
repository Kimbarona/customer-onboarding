using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerRepository repo, ILogger<CustomerService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Customer> Create(CreateCustomerDto dto)
    {
        Validate(dto);

        _logger.LogInformation("Creating customer with email {Email}", dto.Email);

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

        var result = await _repo.Add(customer);

        _logger.LogInformation("Customer created successfully with ID {CustomerId}", result.Id);

        return result;
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
        {
            _logger.LogWarning("Validation failed: First Name is required");
            throw new ArgumentException("First Name is required");
        }

        if (string.IsNullOrWhiteSpace(dto.LastName))
        {
            _logger.LogWarning("Validation failed: Last Name is required");
            throw new ArgumentException("Last Name is required");
        }

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            _logger.LogWarning("Validation failed: Email is required");
            throw new ArgumentException("Email is required");
        }

        if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            _logger.LogWarning("Validation failed: Invalid email format {Email}", dto.Email);
            throw new ArgumentException("Invalid email format");
        }

        var cleanPhone = Regex.Replace(dto.PhoneNumber ?? "", @"\D", "");

        if (!Regex.IsMatch(cleanPhone, @"^09\d{9}$"))
        {
            _logger.LogWarning("Validation failed: Invalid phone number {PhoneNumber}", dto.PhoneNumber);
            throw new ArgumentException("Invalid phone number");
        }

        if (string.IsNullOrWhiteSpace(dto.Signature))
        {
            _logger.LogWarning("Validation failed: Signature is required");
            throw new ArgumentException("Signature is required");
        }
    }
}