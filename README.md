# Accra City Towns API

A RESTful web API for exploring towns, districts, and regions. It provides structured data such as population, geographic coordinates, notable landmarks, and nearby towns — useful for travel planning, business expansion, urban research, and demographic analysis.

Built with **ASP.NET Core 10**, **Entity Framework Core** (PostgreSQL), **ASP.NET Core Identity** with **JWT authentication**, and **Serilog** for logging.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Database Setup](#database-setup)
  - [Configuration](#configuration)
  - [Running the API](#running-the-api)
- [Authentication](#authentication)
- [API Reference](#api-reference)
  - [Auth](#auth)
  - [Regions](#regions)
  - [Districts](#districts)
  - [Towns](#towns)
- [Response Envelope](#response-envelope)
- [Error Handling](#error-handling)
- [Migrations](#migrations)
- [License](#license)

---

## Features

- CRUD operations for **Regions**, **Districts**, and **Towns**
- Relational model: a Region has many Districts and Towns; a District has many Towns
- **JWT-based authentication** with three roles: `USER`, `ADMIN`, and `OWNER`
- Role seeding and role-management endpoints
- Data validation on all request bodies
- Case-insensitive duplicate detection with unique DB indexes
- Consistent JSON response envelope
- Centralized exception handling returning proper HTTP status codes
- Swagger/OpenAPI documentation
- Structured file logging with Serilog

## Tech Stack

| Layer | Technology |
| --- | --- |
| Runtime | .NET 10 |
| Web framework | ASP.NET Core (minimal hosting) |
| ORM | Entity Framework Core 10 + Npgsql |
| Database | PostgreSQL |
| Auth | ASP.NET Core Identity + JWT Bearer |
| API docs | Swashbuckle / Swagger |
| Logging | Serilog |

## Project Structure

```
Accra-City-Towns-Api/
├── AccraCityApi/                    # ASP.NET Core web API (entry point)
│   ├── Controllers/                 # Auth, Region, District, Town controllers
│   ├── ContractMappings/            # Domain <-> DTO mapping extensions
│   ├── Exceptions/                  # Global exception handler
│   ├── Program.cs                   # App composition & pipeline
│   └── ApiEndpoints.cs              # Centralized route constants
├── AccraCity.Application/           # Domain, data access, and business logic
│   ├── Models/                      # Town, Region, District, User entities
│   ├── Database/                    # AppDbContext + design-time factory
│   ├── Repository/                  # EF Core repositories
│   ├── Service/                     # AuthService (Identity + JWT)
│   ├── Dto/                         # Auth request/response DTOs
│   └── Interface/                   # Repository interfaces
├── AccraCityApi.Contracts/          # Shared request/response contracts
│   ├── Requests/                    # Create/Update request models
│   └── Response/                    # Response models + envelope
└── AccraCity.sln
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) (local or remote instance)

### Database Setup

The API ships with an EF Core migration. Create the database (if it does not exist) and apply the migration:

```bash
# 1. Create the database (adjust user/password to match appsettings.json)
createdb AccraCities

# 2. Apply migrations
dotnet ef database update --project AccraCity.Application --startup-project AccraCityApi
```

Alternatively, from inside the `AccraCityApi` folder:

```bash
dotnet ef database update --project ../AccraCity.Application --startup-project .
```

### Configuration

All settings live in `AccraCityApi/appsettings.json`:

```jsonc
{
  "ConnectionStrings": {
    "Default": "User ID=postgres;Password=123;Host=localhost;Port=5432;Database=AccraCities;Pooling=true;"
  },
  "JWT": {
    "Issuer": "https://localhost:7034",
    "Audience": "https://localhost:7035",
    "SecretKey": "<at-least-32-character-secret>"
  },
  "Serilog": {
    // file + console sink configuration.
  }
}
```

> **Security note:** replace the default JWT secret with a strong, random value of at least 32 characters before deploying.

### Running the API

```bash
dotnet run --project AccraCityApi/AccraCityApi.csproj
```

The API listens on:

- HTTP: `http://localhost:5235`
- HTTPS: `https://localhost:7093`

Swagger UI: `http://localhost:5235/swagger/index.html`

> The first time you run the API, call `POST /api/auth/seed-roles` to seed the `USER`, `ADMIN`, and `OWNER` roles.

## Authentication

All auth endpoints return a `AuthServiceResponseDto` with the following shape:

```json
{
  "statusCode": 200,
  "isSucceed": true,
  "message": "User token generated successfully",
  "data": "<jwt-or-null>"
}
```

### 1. Seed roles

```http
POST /api/auth/seed-roles
```

### 2. Register a user

```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Doe",
  "userName": "jane",
  "email": "jane@example.com",
  "password": "password123"
}
```

### 3. Log in to receive a JWT

```http
POST /api/auth/login
Content-Type: application/json

{
  "userName": "jane",
  "password": "password123"
}
```

Include the returned token in subsequent requests:

```http
Authorization: Bearer <your-jwt>
```

### Role management

| Endpoint | Role required | Description |
| --- | --- | --- |
| `POST /api/auth/make_user_admin` | `ADMIN` | Grant the `ADMIN` role |
| `POST /api/auth/make_user_owner` | `OWNER` | Grant the `OWNER` role |
| `POST /api/auth/remove_admin_role` | `ADMIN` | Remove the `ADMIN` role |
| `POST /api/auth/remove_owner_role` | `OWNER` | Remove the `OWNER` role |

Body: `{ "userName": "jane" }`

## API Reference

All responses use the standard [envelope](#response-envelope). A `location` header is returned on creation.

### Auth

| Method | Endpoint | Description |
| --- | --- | --- |
| POST | `/api/auth/seed-roles` | Seed default roles |
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Log in and receive a JWT |
| POST | `/api/auth/make_user_admin` | Make a user an admin (`ADMIN`) |
| POST | `/api/auth/make_user_owner` | Make a user an owner (`OWNER`) |
| POST | `/api/auth/remove_admin_role` | Remove admin role (`ADMIN`) |
| POST | `/api/auth/remove_owner_role` | Remove owner role (`OWNER`) |

### Regions

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/region` | Get all regions |
| GET | `/api/region/{id}` | Get a region by id |
| POST | `/api/region` | Create a region |
| PUT | `/api/region/{id}` | Update a region |
| DELETE | `/api/region/{id}` | Delete a region |

**Create / Update region body:**

```json
{
  "regionName": "Greater Accra"
}
```

### Districts

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/district` | Get all districts |
| GET | `/api/district/{id}` | Get a district by id |
| POST | `/api/district` | Create a district |
| PUT | `/api/district/{id}` | Update a district |
| DELETE | `/api/district/{id}` | Delete a district |

**Create / Update district body:**

```json
{
  "districtName": "Accra Metropolis",
  "regionId": "28d453d5-664a-4829-9e9d-a323ebabb488"
}
```

### Towns

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/town` | Get all towns |
| GET | `/api/town/{id}` | Get a town by id |
| POST | `/api/town` | Create a town |
| PUT | `/api/town/{id}` | Update a town |
| DELETE | `/api/town/{id}` | Delete a town |

**Create / Update town body:**

```json
{
  "townName": "Jamestown",
  "category": "Historic",
  "population": 15000,
  "latitude": 5.5355,
  "longitude": -0.2167,
  "nearbyTowns": ["Accra"],
  "notableLandMarks": ["James Town Lighthouse"],
  "districtId": "632cc841-e070-4012-8c7c-ae555038195d",
  "regionId": "28d453d5-664a-4829-9e9d-a323ebabb488"
}
```

**Get all towns response:**

```json
{
  "statusCode": 200,
  "message": "Towns retrieved successfully.",
  "data": {
    "towns": [
      {
        "id": "cdd15c8a-6cf2-43ea-bd02-285144cfc93d",
        "townName": "Jamestown",
        "category": "Historic",
        "population": 15000,
        "latitude": 5.5355,
        "longitude": -0.2167,
        "createdAt": "2026-07-31T18:13:56.837952Z",
        "lastModifiedAt": "2026-07-31T18:13:56.949794Z",
        "nearbyTowns": ["Accra"],
        "notableLandMarks": ["James Town Lighthouse"],
        "districtId": "632cc841-e070-4012-8c7c-ae555038195d",
        "regionId": "28d453d5-664a-4829-9e9d-a323ebabb488"
      }
    ]
  }
}
```

## Response Envelope

Every endpoint returns a consistent envelope:

```json
{
  "statusCode": 200,
  "message": "Towns retrieved successfully.",
  "data": { }
}
```

| Field | Description |
| --- | --- |
| `statusCode` | HTTP status code |
| `message` | Human-readable result message |
| `data` | Payload (object, array, or `null`) |

### Common status codes

| Code | Meaning |
| --- | --- |
| `200` | OK |
| `201` | Created (with `Location` header) |
| `400` | Invalid request or validation failure |
| `401` | Missing or invalid JWT |
| `403` | Authenticated but not authorized for the role |
| `404` | Resource not found |
| `409` | Duplicate resource (case-insensitive name conflict) |
| `500` | Unexpected server error |

## Error Handling

- **Validation failures** are handled automatically by `[ApiController]` and return `400` with field-level errors.
- **Unhandled exceptions** are caught by a global exception handler (`GlobalExceptionHandler`) and returned as `500` with the standard envelope — exception details are only written to the logs, never exposed to the client.

## Migrations

Migrations live in `AccraCity.Application/Migrations`. To add a new migration:

```bash
dotnet ef migrations add <MigrationName> --project AccraCity.Application --startup-project AccraCityApi
```

## License

[MIT](LICENSE)
