# 🛒 ShopNest

A scalable **E-Commerce Web API** built with **ASP.NET Core .NET 8**, designed with a clean, layered architecture to provide a maintainable foundation for modern online shopping applications.

The project focuses on building a structured backend for product management, business logic, data persistence, and API-based communication.

---

## 🚀 Overview

**ShopNest** is an e-commerce backend application developed using **ASP.NET Core Web API**.

The system follows a layered architecture that separates domain entities, business logic, persistence, service abstractions, and API presentation.

The architecture is designed to promote:

* Separation of concerns
* Maintainability
* Scalability
* Testability
* Reusability
* Clean dependency management

---

## ✨ Key Features

* 🛍️ Product management
* 📦 Product and catalog operations
* 🗄️ SQL Server database integration
* 🔄 Entity Framework Core
* ⚡ Redis integration for caching
* 📚 Specification-based querying
* 🔀 Repository pattern
* 🧩 Service abstraction layer
* 🔐 Structured API architecture
* 📖 Swagger / OpenAPI documentation
* 🛠️ Centralized application configuration
* 🖼️ Product image management

---

## 🛠️ Technologies

### Backend

* **C#**
* **ASP.NET Core Web API**
* **.NET 8**
* **Entity Framework Core**
* **LINQ**
* **Dependency Injection**

### Database & Data Access

* **Microsoft SQL Server**
* **Entity Framework Core SQL Server Provider**
* **EF Core Migrations**
* **Repository Pattern**
* **Specification Pattern**

### Caching

* **Redis**
* **StackExchange.Redis**

### API Documentation

* **Swagger**
* **OpenAPI**

The project currently targets **.NET 8**, uses Entity Framework Core SQL Server, and includes StackExchange.Redis for Redis integration.

---

# 🏗️ Architecture

ShopNest is organized into multiple projects with clear responsibilities:

ShopNest
│
├── ShopNest.Domain
│   ├── Entities
│   ├── Contracts
│   └── Domain Models
│
├── ShopNet.Services
│   └── Service Implementations
│
├── ShopNest.Services.Abstraction
│   └── Service Interfaces
│
├── ShopNest.Presistence
│   ├── Data
│   ├── Repositories
│   ├── SpecificationEvaluator
│   └── Database Configuration
│
├── ShopNest.Presentation
│   ├── Controllers
│   └── API Presentation Logic
│
├── ShopNest.Shared
│   └── Shared Models & Utilities
│
└── ShopNest.PL
    ├── Program.cs
    ├── Extensions
    ├── Custom Middlewares
    └── Configuration


The repository currently contains these main projects: `ShopNest.Domain`, `ShopNest.PL`, `ShopNest.Presentation`, `ShopNest.Presistence`, `ShopNest.Services.Abstraction`, `ShopNest.Shared`, and `ShopNet.Services`.

---

## 📦 Domain Layer

### `ShopNest.Domain`

Contains the core business entities and domain contracts.

```text
ShopNest.Domain
│
├── Entities
└── Contracts
```

This layer is independent from infrastructure concerns and represents the core concepts of the application.

---

## 🔧 Service Layer

### `ShopNest.Services.Abstraction`

Contains service interfaces and abstractions used to decouple the business logic from its implementations.

This allows the application to depend on **abstractions rather than concrete implementations**.

### `ShopNet.Services`

Contains the concrete implementations of the application's services and business operations.

---

## 🗄️ Persistence Layer

### `ShopNest.Presistence`

Responsible for communication with the database and infrastructure-related operations.

It contains:

* Entity Framework Core configuration
* Repositories
* Database context
* Specification evaluator
* Data persistence logic

The project also includes **SQL Server support through Entity Framework Core** and **Redis integration through StackExchange.Redis**.

---

## 🌐 Presentation Layer

### `ShopNest.Presentation`

Responsible for exposing the application's API endpoints through controllers.

```text
ShopNest.Presentation
│
├── Attributes
└── Controllers
```

This keeps API endpoint logic separated from the core business and persistence layers.

---

## ⚙️ API / Application Layer

### `ShopNest.PL`

Acts as the executable ASP.NET Core application.

It contains:

* `Program.cs`
* Application configuration
* Custom middleware
* Extension methods
* Static product images
* Environment configuration

The project is configured as an ASP.NET Core Web SDK application targeting **.NET 8**.

---

# 🔍 Specification Pattern

ShopNest includes a **Specification Evaluator**, allowing database queries to be constructed in a reusable and organized way.

Instead of placing complex filtering and querying logic directly inside controllers or repositories, specifications can encapsulate query requirements.

This helps support:

* Filtering
* Sorting
* Pagination
* Reusable queries
* Cleaner repository implementations

---

# ⚡ Redis Caching

The persistence layer includes **StackExchange.Redis**, providing Redis support for high-performance caching.

Redis can be used to reduce repeated database queries and improve response times for frequently accessed data.

Possible caching scenarios include:

* Product catalogs
* Frequently requested data
* Session-related information
* Temporary application data

---

# 📖 API Documentation

ShopNest uses **Swagger / OpenAPI** through `Swashbuckle.AspNetCore`.

This provides an interactive interface for exploring and testing API endpoints during development.

After running the application, Swagger can typically be accessed through:

```text
https://localhost:<port>/swagger
```

---

# ⚙️ Getting Started

## Prerequisites

Make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Microsoft SQL Server
* SQL Server Management Studio
* Redis
* Visual Studio 2022 or VS Code
* Git

---

## 1️⃣ Clone the Repository

```bash
git clone https://github.com/2302305/ShopNestProject.git
```

```bash
cd ShopNestProject
```

---

## 2️⃣ Configure the Database

Open:

```text
ShopNest.PL/appsettings.json
```

Configure your SQL Server connection string.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ShopNest;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> Use the connection string appropriate for your local SQL Server configuration.

---

## 3️⃣ Configure Redis

Make sure Redis is running locally.

A typical Redis connection looks like:

```text
localhost:6379
```

Configure it according to the application's configuration.

---

## 4️⃣ Restore Dependencies

From the solution directory:

```bash
dotnet restore
```

---

## 5️⃣ Apply Database Migrations

Run:

```bash
dotnet ef database update
```

If the startup and persistence projects need to be specified:

```bash
dotnet ef database update \
    --project ShopNest.Presistence \
    --startup-project ShopNest.PL
```

---

## 6️⃣ Build the Project

```bash
dotnet build
```

---

## 7️⃣ Run the API

```bash
dotnet run --project ShopNest.PL
```

The API will then be available through the configured localhost port.

Swagger can be used to explore and test the available endpoints.

---

# 📂 Project Structure

| Project                         | Responsibility                             |
| ------------------------------- | ------------------------------------------ |
| `ShopNest.Domain`               | Core entities and domain contracts         |
| `ShopNest.Services.Abstraction` | Service interfaces and abstractions        |
| `ShopNet.Services`              | Business/service implementations           |
| `ShopNest.Presistence`          | Database, repositories and infrastructure  |
| `ShopNest.Presentation`         | API controllers                            |
| `ShopNest.Shared`               | Shared models and utilities                |
| `ShopNest.PL`                   | ASP.NET Core application and configuration |

---

# 🎯 Project Goals

The main objectives of ShopNest are to demonstrate practical backend engineering concepts, including:

* Building RESTful APIs with ASP.NET Core
* Applying layered architecture
* Applying separation of concerns
* Implementing Repository Pattern
* Implementing Specification Pattern
* Working with Entity Framework Core
* Integrating SQL Server
* Integrating Redis caching
* Applying Dependency Injection
* Creating reusable service abstractions
* Documenting APIs with Swagger
* Designing maintainable backend systems

---

# 🔮 Future Improvements

Potential improvements include:

* 🔐 JWT authentication and authorization
* 👤 User and role management
* 🛒 Shopping cart
* 📦 Order management
* 💳 Payment integration
* ⭐ Product reviews and ratings
* ❤️ Wishlist functionality
* 🔔 Notifications
* 📧 Email services
* 📊 Admin dashboard
* 🔎 Advanced product search
* 📈 Sales analytics
* 🐳 Docker support
* ☁️ Cloud deployment
* 🧪 Automated unit and integration testing
* ⚡ Advanced Redis caching strategies

---

# 👨‍💻 Author

**Saif Hamza**

Computer Science Graduate | Backend .NET Developer

### Technologies

`C#` · `.NET 8` · `ASP.NET Core` · `Web API` · `Entity Framework Core` · `SQL Server` · `Redis` · `Swagger` · `LINQ` · `Repository Pattern` · `Specification Pattern`

---

## 📄 License

This project is developed for educational and portfolio purposes.
