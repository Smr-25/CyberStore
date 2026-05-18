# CyberStore

Modern multi-layered e-commerce web application built with **ASP.NET Core MVC (.NET 10)** and **Entity Framework Core 10**.

## 🚀 Tech Stack

- **.NET SDK:** 10.x
- **Target Framework:** `net10.0`
- **Backend:** ASP.NET Core MVC
- **Data Access:** Entity Framework Core 10 + SQL Server provider
- **Architecture:** Layered (Web + Application + Data + Core)

## 📦 Solution Structure

```text
CyberStore.sln
├── CyberStore/             # ASP.NET Core MVC web app (UI + controllers + DI)
├── CyberStore.Application/ # Business logic and services
├── CyberStore.Data/        # EF Core DbContext, repositories, migrations
└── CyberStore.Core/        # Domain entities
```

## ✨ Main Features

- Product listing with:
  - search
  - category filtering
  - sorting
  - pagination
- Product details page
- Shopping cart management
- Wishlist management
- Contact form (stored in database)
- Order creation from cart
- Order history and order status updates
- Seed data for categories, brands, and sample products
- Automatic database migration at startup

## 🧠 Architecture Overview

- **Core Layer:** domain entities (`Product`, `Order`, `CartItem`, etc.)
- **Data Layer:** `AppDbContext`, generic repository, unit of work, concrete repositories
- **Application Layer:** service interfaces + implementations for product/cart/wishlist/contact/order flows
- **Presentation Layer:** MVC controllers and Razor views

Dependency flow:

`Web -> Application -> Data -> Core`

## ⚙️ Prerequisites

- **.NET 10 SDK** installed (`dotnet --version` should return 10.x)
- SQL Server-compatible connection string

## 🔧 Configuration

Connection string is configured in:

- `CyberStore/CyberStore/appsettings.json`

Default key:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=CyberStore.db"
}
```

> Update `DefaultConnection` to your SQL Server instance for local development.

## ▶️ Run Locally

From the solution directory (where `CyberStore.sln` is located):

```bash
dotnet restore
dotnet build
dotnet run --project ./CyberStore/CyberStore.csproj
```

The app runs with launch profile URLs from `launchSettings.json`, including:

- `https://localhost:7007`
- `http://localhost:5258`

## 🗄️ Database & Migrations

- EF Core migrations are in `CyberStore.Data/Migrations`
- App applies migrations automatically on startup (`context.Database.Migrate()`)

If needed, you can manage migrations manually:

```bash
dotnet ef migrations add <MigrationName> --project ./CyberStore.Data --startup-project ./CyberStore
dotnet ef database update --project ./CyberStore.Data --startup-project ./CyberStore
```

## ✅ Build / Test Status

Current repository baseline checks:

- `dotnet build CyberStore.sln` ✅
- `dotnet test CyberStore.sln` ✅ (no dedicated test project currently)

## 📌 Notes

- This project currently uses a demo user id (`demo-user-1`) for cart/wishlist/order flows.
- Suitable base for extending authentication, payment integration, and admin features.
