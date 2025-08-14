
````markdown
# SmartHRM

SmartHRM is a modular Human Resource Management system built using **.NET 8**, **Entity Framework Core**, **SQL Server**, and **Razor Pages**. The solution follows a layered architecture with separate projects for Core, Application, Infrastructure, API, and Client.

---

## Project Structure

- **SmartHRM.Core**  
  Contains the domain entities and core interfaces.

- **SmartHRM.Application**  
  Contains DTOs, service interfaces, and business logic.

- **SmartHRM.Infrastructure**  
  Handles data access, Entity Framework Core, and SQL Server integration. Also contains EF Core migrations.

- **SmartHRM.API**  
  Exposes RESTful APIs to interact with the backend.

- **SmartHRM.Client**  
  Razor Pages frontend application to consume API endpoints.

---

## Project References

To set up project references, run the following commands:

```bash
dotnet add SmartHRM.Infrastructure/SmartHRM.Infrastructure.csproj reference SmartHRM.Core/SmartHRM.Core.csproj
dotnet add SmartHRM.Application/SmartHRM.Application.csproj reference SmartHRM.Core/SmartHRM.Core.csproj
dotnet add SmartHRM.API/SmartHRM.API.csproj reference SmartHRM.Infrastructure/SmartHRM.Infrastructure.csproj
dotnet add SmartHRM.API/SmartHRM.API.csproj reference SmartHRM.Application/SmartHRM.Application.csproj
````

---

## Packages and Dependencies

### Infrastructure & API (Entity Framework Core + SQL Server)

```bash
dotnet add SmartHRM.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add SmartHRM.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add SmartHRM.Infrastructure package Microsoft.EntityFrameworkCore.Design

dotnet add SmartHRM.API package Microsoft.EntityFrameworkCore
dotnet add SmartHRM.API package Microsoft.EntityFrameworkCore.SqlServer
```

### Client (Razor Pages)

```bash
dotnet add SmartHRM.Client package Microsoft.Extensions.Http
```

---

## Running the Applications

### Run the Client

```bash
cd SmartHRM.Client
dotnet run
```

### Run the API

```bash
cd SmartHRM.API
dotnet run
```

---

## Notes

* The **Infrastructure project** contains the database context and is responsible for migrations.
* The **Client project** communicates with the API using HTTP clients.
* Ensure SQL Server is installed and connection strings are correctly configured in `appsettings.json`.

---

