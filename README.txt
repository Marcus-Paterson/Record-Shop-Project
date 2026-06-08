# Northcoders Record Shop Backend  

Welcome to the Northcoders Record Shop Backend — a production‑style ASP.NET Core Web API that manages a digital record collection.
This project was built as a solo backend assignment, focusing on clean architecture, maintainability, and real‑world API design.

---

# 🚀 Overview
The Record Shop API allows clients to:
> Add new records
> View all records
> Retrieve a record by ID
> Update record details
> Delete records
> Check API + database health
> Explore endpoints via Swagger UI


# 🧰 Tech Stack

  | Area | Technologies |
| --- | --- |
| **Framework** | ASP.NET Core Web API (.NET 8) |
| **Language** | C# |
| **ORM** | Entity Framework Core |
| **Databases** | SQLite (Dev), SQL Server (Prod) |
| **Testing** | NUnit, Moq, Shouldly |
| **Tools** | Swagger, HealthChecks, HttpClient |


# 🎯 Core Features
🎵 Record Management (CRUD)
> Create new records
> Read all records or a single record by ID
> Update existing records
> Delete records
> Validation + error handling included

🗄️ Database Layer
> SQLite for development
> SQL Server for production
> Automatic schema creation
> Repository pattern for clean separation

📘 API Documentation
> Full Swagger UI
> Example requests + responses
> Error codes documented

❤️ Health Monitoring
/health endpoint

🏗️ Architecture
> The project uses a clean, decoupled structure:
Controllers
 - Handle HTTP requests
 - Return DTOs and status codes
 - No business logic

Services
 - Contain business rules
 - Validate inputs
 - Coordinate repository operations

Repositories
 - Abstract EF Core
 - Handle database queries
 - Return domain models

EF Core
 - Handles migrations
 - Maps models to database tables

🧪 Testing
The backend includes automated test coverage across core layers.

Repository Tests
- Use in‑memory SQLite
- Validate CRUD operations

Service Tests
- Mock repositories with Moq
- Validate business logic

Controller Tests
- Use mocked services
- Validate routing + status codes

Testing Tools
- NUnit — test framework
- Moq — mocking dependencies
- Shouldly — readable assertions

🗄️ Database Configuration
> Development (SQLite)
> Auto‑created using EnsureCreated()
> Lightweight and fast

Production (SQL Server)
> Uses EF Core migrations
>Supports scaling + deployment

🛠️ Planned Enhancements
Pagination + filtering
> Sorting (by year, price, artist)
> Search by title/artist
> Record artwork/image support
> Seeding sample data


