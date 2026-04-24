# Customer Onboarding API

ASP.NET Core Web API with Clean Architecture.

## Project Structure

```
customer-onboarding/
├── src/
│   ├── Api/              # ASP.NET Core Web API
│   ├── Application/      # Business logic & DTOs
│   ├── Domain/           # Entities
│   ├── Infrastructure/   # Database & Repositories
│   └── Tests/            # Unit tests
└── logs/                 # Application logs
```

## Prerequisites

- .NET 10.0 SDK
- SQLite (bundled with .NET)

## Run Instructions

### Backend

```bash
cd src/Api
dotnet run
```

The API will be available at `http://localhost:5237`

### Run Tests

```bash
cd src/Tests
dotnet test
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/customers` | Create a new customer |
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get customer by ID |

## Request Body (POST /api/customers)

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "09123456789",
  "signature": "data:image/png;base64,..."
}
```

## Validation Rules

- **FirstName**: Required
- **LastName**: Required
- **Email**: Required, valid email format
- **PhoneNumber**: Required, Philippine format (09xxxxxxxxx)
- **Signature**: Required

## Logs

Application logs are stored in `src/Api/logs/app.log`

## Technology Stack

- ASP.NET Core 10.0
- Entity Framework Core (SQLite)
- Serilog (Logging)
- xUnit (Testing)
- Moq (Mocking)