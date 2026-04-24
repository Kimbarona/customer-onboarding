using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain;
using Xunit;
using Moq;

namespace Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repoMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _repoMock = new Mock<ICustomerRepository>();
        _service = new CustomerService(_repoMock.Object);
    }

    [Fact]
    public async Task Create_ShouldThrowException_WhenEmailIsEmpty()
    {
        var dto = new CreateCustomerDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "",
            PhoneNumber = "123",
            Signature = "sig"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Create(dto));
    }

    [Fact]
    public async Task Create_ShouldReturnCustomer_WhenValidInput()
    {
        var dto = new CreateCustomerDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "jd@test.com",
            PhoneNumber = "09123456789",
            Signature = "sig"
        };

        _repoMock.Setup(x => x.Add(It.IsAny<Customer>()))
                 .ReturnsAsync((Customer c) => c);

        var result = await _service.Create(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Email, result.Email);
        _repoMock.Verify(x => x.Add(It.IsAny<Customer>()), Times.Once);
    }
}