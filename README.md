# 🛒 CyberStore

CyberStore is a modern ASP.NET Core MVC e-commerce sample application built with **.NET 10** using a clean layered architecture approach.

The project simulates a complete electronic products store and includes essential e-commerce workflows such as product browsing, shopping cart management, wishlist functionality, order processing, and contact messaging.

---

# 📌 Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Database & Migrations](#database--migrations)
- [Configuration](#configuration)
- [Available Pages & Flows](#available-pages--flows)
- [Developer Commands](#developer-commands)
- [Troubleshooting](#troubleshooting)
- [Current Limitations](#current-limitations)

---

# 📖 Overview

CyberStore is designed as a layered ASP.NET Core MVC application following clean architecture principles.

The solution is separated into the following layers:

- **Web/UI Layer** — ASP.NET Core MVC
- **Application Layer** — Business logic & services
- **Data Layer** — Entity Framework Core, repositories & database access
- **Core Layer** — Domain entities & models

---

# ⚙️ Tech Stack

| Technology | Description |
|------------|-------------|
| .NET SDK | 10.0.x |
| Framework | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 10 |
| Database | SQL Server |
| Patterns | Generic Repository, Unit of Work, Service Layer |

---

# 🏗️ Architecture

Project structure inside the repository:

```text
CyberStore.sln
├── CyberStore/              -> ASP.NET Core MVC (UI, Controllers, Views)
├── CyberStore.Application/  -> Application services & business logic
├── CyberStore.Data/         -> DbContext, Repositories, Migrations
└── CyberStore.Core/         -> Domain entities
```

## Layer Responsibilities

### 🔹 CyberStore (Web)
Handles HTTP requests, MVC controllers, routing, and UI rendering.

### 🔹 CyberStore.Application
Contains business logic, application services, and use-case implementations.

### 🔹 CyberStore.Data
Responsible for database operations, EF Core configurations, repositories, and migrations.

### 🔹 CyberStore.Core
Contains core domain entities such as:

- Product
- Order
- CartItem
- Category
- WishlistItem

---

# ✨ Features

- Product listing
- Product search
- Category filtering
- Product sorting (name, price, newest)
- Pagination
- Shopping cart management
- Wishlist management
- Contact form submission
- Order creation & checkout flow
- Order details & status tracking
- Seed/sample data generation

---

# 📋 Requirements

Before running the project, make sure you have:

- **.NET 10 SDK**
- **SQL Server** instance (local or remote)

> The application automatically attempts to apply migrations on startup using `context.Database.Migrate()`.

---

# 🚀 Getting Started

Run the following commands from the repository root:

```bash
cd CyberStore
dotnet restore
dotnet build CyberStore.sln
dotnet run --project CyberStore/CyberStore.csproj
```

After the application starts, open the URL displayed in the terminal (usually):

```text
https://localhost:xxxx
```

---

# 🗄️ Database & Migrations

Migration files are located under:

```text
CyberStore.Data/Migrations
```

## Configure Connection String

Update the `DefaultConnection` value inside:

```text
CyberStore/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=CyberStoreDb;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True;"
}
```

---

## Manual Migration Commands

```bash
cd CyberStore

# Create a new migration
dotnet ef migrations add <MigrationName> \
  --project CyberStore.Data/CyberStore.Data.csproj \
  --startup-project CyberStore/CyberStore.csproj

# Apply migrations
dotnet ef database update \
  --project CyberStore.Data/CyberStore.Data.csproj \
  --startup-project CyberStore/CyberStore.csproj
```

---

# ⚡ Configuration

Configuration files:

- `CyberStore/appsettings.json`
- `CyberStore/appsettings.Development.json`

Optional local configuration:

- `CyberStore/appsettings.Mac.json`

You may create your own local override configuration file if needed.

---

# 🌐 Available Pages & Flows

| Route | Description |
|------|-------------|
| `/` | Home page & product listing |
| `/Store/Details/{id}` | Product details |
| `/Store/Cart` | Shopping cart |
| `/Store/Wishlist` | Wishlist |
| `/Order/Create` | Checkout page |
| `/Order/Details/{id}` | Order details |
| `/Order/History` | User order history |
| `/Order/AllOrders` | All orders |
| `/Home/Contact` | Contact page |

### Important POST Actions

- `/Store/AddToCart`
- `/Store/RemoveFromCart`
- `/Store/AddToWishlist`
- `/Store/RemoveFromWishlist`
- `/Order/Create`
- `/Order/UpdateStatus`
- `/Home/SubmitContact`

---

# 🧑‍💻 Developer Commands

```bash
cd CyberStore

dotnet restore
dotnet build CyberStore.sln
dotnet test CyberStore.sln
```

---

# 🛠️ Troubleshooting

## Database Connection Issues

- Verify the `DefaultConnection` string
- Ensure SQL Server is accessible

## Migration Problems

- Make sure `dotnet ef` is installed
- Verify startup project and data project parameters

## HTTPS / Certificate Issues

Refresh development certificates:

```bash
dotnet dev-certs https --trust
```

---

# ⚠️ Current Limitations

- Authentication & authorization are not implemented yet
- Demo user (`demo-user-1`) is currently used for cart/order operations
- `dotnet test` works, but there is no dedicated test project yet
- The project is configured specifically for SQL Server

---

# 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Submit a Pull Request

---

# 📄 License

This project is provided for educational and demonstration purposes.
