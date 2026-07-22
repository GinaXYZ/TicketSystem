# TicketSystem

A web-based ticket management application built with ASP.NET Core MVC and SQL Server. The project demonstrates a layered application structure with authentication, repositories, services, view models, database access and REST endpoints.

## Features

- User login with cookie-based authentication
- Create and display support tickets
- Ticket categorisation, priority and status handling
- Repository and service layers for separating data access from business logic
- SQL Server database integration with Entity Framework Core
- REST endpoints for retrieving and creating tickets
- Swagger/OpenAPI support in the development environment
- Automatic sample data creation when the database is empty

## Tech Stack

- C#
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Cookie Authentication
- BCrypt
- Swagger / OpenAPI

## Project Structure

```text
TicketSystem/
├── Controllers/        MVC controllers
├── Data/               Database context and repositories
├── Models/             Domain models
├── Services/           Business logic
├── ViewModels/         Data models used by views and API requests
├── Views/              Razor views
├── wwwroot/            Static assets
└── Program.cs          Application configuration and endpoints
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/tickets` | Returns all tickets |
| POST | `/api/tickets` | Creates a new ticket |

Swagger UI is available in development mode.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022, Rider or Visual Studio Code

### Installation

```bash
git clone https://github.com/GinaXYZ/TicketSystem.git
cd TicketSystem/TicketSystem
dotnet restore
```

Add a valid SQL Server connection string named `DefaultConnection` to `appsettings.json` or use .NET user secrets.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TicketSystem;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Apply the database migrations if required:

```bash
dotnet ef database update
```

Start the application:

```bash
dotnet run
```

Open the local URL shown in the terminal. In development mode, Swagger is available at `/swagger`.

## Architecture

The application uses a layered approach:

1. Controllers and API endpoints receive requests.
2. View models define the required input and output data.
3. Services contain the application logic.
4. Repositories handle database operations.
5. Entity Framework Core maps the domain models to SQL Server.

This structure keeps presentation, business logic and persistence concerns separated and makes the project easier to maintain and extend.

## Possible Improvements

- Add automated unit and integration tests
- Expand role-based authorisation
- Add ticket comments and file attachments
- Add filtering, search and pagination
- Add Docker support
- Add a CI/CD workflow with GitHub Actions
- Deploy the application to Azure or AWS

## Project Context

This project was created during my training as an application developer to practise ASP.NET Core, authentication, database access, REST APIs and layered software architecture.

## Author

**Gina**  
[GitHub Profile](https://github.com/GinaXYZ) · [Portfolio](https://ginaxyz.net)
