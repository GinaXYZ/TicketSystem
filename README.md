# TicketSystem

TicketSystem is a small help desk application built with ASP.NET Core MVC and SQL Server. It provides a browser-based interface for signing in, viewing tickets and managing their status, priority, category and due date.

The project was developed during my training as an application developer and focuses on database-backed web development, authentication and separating application logic into clear layers.

## Project status

The main ticket workflow is implemented and can be run locally. The application is a learning and portfolio project, not a production-ready help desk system.

The current authentication flow is intended for local development. Password handling and the development account must be replaced before the application is used in a real environment.

## Features

- Cookie-based user authentication
- Ticket overview with open, in-progress and closed ticket counts
- Ticket creation, editing and deletion
- Sorting by status, priority and ticket ID
- Pagination of the ticket list
- Ticket lookup by ID
- Categories, priorities, statuses and optional due dates
- SQL Server persistence through Entity Framework Core
- Repository and service layers
- REST endpoints for reading and creating tickets
- Swagger/OpenAPI documentation in development mode
- Initial sample tickets for an empty database

## Technology

- C#
- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- SQL Server / SQL Server LocalDB
- Cookie Authentication
- Swagger / OpenAPI

## Architecture

The application follows a simple layered structure:

```text
Browser / API client
        |
Controllers and API endpoints
        |
Services
        |
Repositories
        |
Entity Framework Core
        |
SQL Server
```

Controllers handle HTTP requests and page navigation. Business operations are kept in the service layer, while repositories are responsible for database access. View models define the data used by forms and API requests.

## API

| Method | Endpoint | Purpose |
| --- | --- | --- |
| `GET` | `/api/tickets` | Return all tickets |
| `POST` | `/api/tickets` | Create a ticket |

When the application runs in the development environment, Swagger UI is available at `/swagger`.

## Getting started

### Requirements

- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio, Rider or Visual Studio Code

### Run the application

```bash
git clone https://github.com/GinaXYZ/TicketSystem.git
cd TicketSystem/TicketSystem
dotnet restore
dotnet ef database update
dotnet run
```

The repository contains a LocalDB connection string for local development. To use another SQL Server instance, change `ConnectionStrings:DefaultConnection` in `appsettings.json` or provide it through .NET user secrets.

Open the local address printed in the terminal after the application starts.

## Project structure

```text
TicketSystem/
├── Controllers/       MVC request handling and authentication
├── Data/              DbContext and repository implementations
├── Migrations/        Entity Framework Core migrations
├── Models/            Database entities
├── Services/          Ticket-related application logic
├── ViewModels/        Form and API request models
├── Views/             Razor pages
├── wwwroot/           Styles, scripts and static files
└── Program.cs         Dependency injection and app configuration
```

## Known limitations

- Authentication is currently designed for development use
- Automated tests are not included yet
- The API currently covers only listing and creating tickets
- There is no attachment or comment system
- Deployment and container configuration are not included

## Possible next steps

- Replace the development login with ASP.NET Core Identity
- Hash and verify all stored passwords securely
- Add role-based permissions
- Add ticket comments and attachments
- Add API endpoints for updates and deletion
- Add unit and integration tests
- Add Docker and a GitHub Actions workflow
- Deploy the application to a cloud environment

## Author

Gina  
[GitHub](https://github.com/GinaXYZ) | [Portfolio](https://ginaxyz.net)
