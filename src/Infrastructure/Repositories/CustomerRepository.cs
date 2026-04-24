using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> Add(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetById(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }
}