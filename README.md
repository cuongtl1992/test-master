# TestMaster

TestMaster is a comprehensive test management system designed to help quality assurance teams manage test cases, test suites, execution, reporting, and more with AI-assisted capabilities.

## Project Overview

TestMaster is a monolithic application with clearly defined modules that can be easily maintained and potentially migrated to microservices in the future if needed. The system manages the entire lifecycle of testing, from test case creation to execution and reporting.

## Technology Stack

### Backend
- C# .NET 8.0
- ASP.NET Core 8.0 for Web API
- Entity Framework Core 8.0 (PostgreSQL)
- MediatR for CQRS
- Serilog for structured logging
- FluentValidation
- xUnit for testing

### Frontend (Planned)
- Angular 19
- TypeScript 5.0+
- NgRx for state management
- RxJS for async operations
- Angular Material or PrimeNG for UI components
- D3.js/Chart.js for visualization

### Database
- PostgreSQL 14+
- Redis for caching

### Infrastructure
- Docker and Docker Compose
- GitHub Actions for CI/CD

## Architecture

TestMaster follows a modular monolithic architecture with clean separation of concerns:

- **TestMaster.Api**: API entry point and controllers
- **TestMaster.Core**: Business logic, domain models, and interfaces
- **TestMaster.Infrastructure**: Data access, external services, and implementation details
- **TestMaster.Tests**: Unit and integration tests

### Modules

The application is divided into the following business modules:

1. **Test Management**: Handling test cases, test suites, and their relationships
2. **User Management**: User accounts, roles, and permissions
3. **Execution**: Test execution, tracking, and results
4. **Reporting**: Analytics, metrics, and reporting
5. **AI Integration**: AI-assisted test case generation and analysis
6. **Test Plan Hierarchy**: Test plans, cycles, and their hierarchical structure

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL 14+
- Redis (optional for caching)

### Setup

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/test-master.git
   cd test-master
   ```

2. Configure database connection in `backend/src/TestMaster.Api/appsettings.json`

3. Build and run the backend:
   ```
   cd backend
   dotnet build
   cd src/TestMaster.Api
   dotnet run
   ```

4. Create database migrations:
   ```
   cd backend/src/TestMaster.Api
   dotnet ef migrations add InitialCreate -p ../TestMaster.Infrastructure/TestMaster.Infrastructure.csproj
   dotnet ef database update
   ```

### Running with Docker

To run the application using Docker:

```
docker-compose up -d
```

## Development Workflow

### Backend

1. Make changes to the code
2. Run tests: `dotnet test`
3. Build the solution: `dotnet build`
4. Run the API: `dotnet run --project src/TestMaster.Api/TestMaster.Api.csproj`

## API Documentation

Once the application is running, API documentation is available at:

- Swagger UI: `https://localhost:5001/swagger`

## Contributing

Please read our contributing guidelines before submitting pull requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
