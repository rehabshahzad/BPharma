# BPharma

BPharma is a backend Pharmacy Management System built to manage the complete pharmacy workflow, from purchasing items from suppliers to selling them to customers.

The system supports batch-based inventory, purchases, sales, customer returns, supplier returns, employee management, audit tracking, and inventory movements.

BPharma follows the **FEFO (First Expired, First Out)** inventory strategy, where the available batch with the earliest expiry date is prioritized first.

---

# Features

- Batch-based inventory management
- FEFO-based stock allocation
- Purchase and supplier management
- Sales and customer management
- Customer returns
- Supplier returns
- Inventory movement tracking
- Employee management
- Role-based access control
- JWT authentication
- Secure password hashing
- Audit fields for important records
- Product classification using Brand, Category, and Formula
- Rack, shelf, and lane location tracking

---

# User Roles

BPharma supports three employee roles:

- **Admin** — Full system access and employee management
- **Pharmacist** — Customers, sales, and customer returns
- **Inventory Manager** — Suppliers, purchases, inventory, and supplier returns

Authorization is enforced using role-based access control.

---

# Authentication and Security

BPharma uses **JWT (JSON Web Token) authentication** to protect API endpoints.

Passwords are never stored in plain text. Password hashing is implemented using **PBKDF2** through `Rfc2898DeriveBytes` with:

- SHA-256
- 100,000 iterations
- 16-byte random salt
- 32-byte derived hash

Each password receives a unique cryptographically generated salt.

The stored password format is:

```text
iterations.salt.hash
```
# Architecture

BPharma follows a layered architecture:

Client / API Consumer
        ↓
Pharma.WebApi
        ↓
Pharma.BLL
        ↓
Pharma.DAL
        ↓
Entity Framework 6
        ↓
MySQL

Authentication requests are processed through the API and business logic layers, including AuthService.Login().


# Project Structure
BPharma
│
├── Pharma.BLL
│   ├── Security
│   └── Services
│
├── Pharma.DAL
│   ├── Context
│   ├── Migrations
│   └── Repositories
│
├── Pharma.Entity
│   ├── Entities
│   └── Enums
│
├── Pharma.UnitTests
│
├── Pharma.WebApi
│   ├── App_Start
│   ├── Controllers
│   ├── DTOs
│   ├── Models
│   ├── Security
│   ├── AppSecrets.example.config
│   ├── ConnectionStrings.example.config
│   ├── Global.asax
│   └── Web.config
│
└── docs
    └── BPharma_ERD.svg
    
# Layer Responsibilities
Pharma.Entity

Defines the application's core entities, relationships, and enums.

-Pharma.DAL

Handles database access using Entity Framework 6, including the DbContext, repositories, and Code First migrations.

-Pharma.BLL

Contains business logic, validation, FEFO inventory rules, service operations, and password security.

-Pharma.WebApi

Exposes REST API endpoints, contains DTOs and controllers, and handles JWT authentication and role-based authorization.

-Pharma.UnitTests

Contains automated tests for validating application behavior and business logic.

# Database Design

BPharma uses a relational database with separate entities for inventory, transactions, employees, customers, suppliers, and returns.

The ERD contains the main relationships between customers, sales, returns, purchases, batches, inventory movements, supplier-item mappings, formulas, categories, and supporting item entities.

**Open Full ERD: \docs\BPharma_ERD.drawio.svg"**

# FEFO Inventory Management

BPharma uses First Expired, First Out (FEFO) for stock allocation.

If the same item exists in multiple batches, the available batch with the earliest valid expiry date is used first.

Example:

Batch A
Expiry: January 2027

Batch B
Expiry: March 2027

Stock from Batch A is allocated before Batch B.

This helps reduce expired inventory and improves stock rotation.

# Batch Allocation

Items are stored in batches so that inventory can be tracked by expiry date.

A sale item can be fulfilled using one or more batches through BatchAllocation.

Sale
 ↓
SaleItem
 ↓
BatchAllocation
 ↓
Batch

This preserves batch-level traceability for every sale.

# Inventory Movements

Inventory changes are recorded using InventoryMovement.

Typical movement types include:

PurchaseIn
SaleOut
CustomerReturnIn
SupplierReturnOut
AdjustmentIn
AdjustmentOut

This provides an audit trail of how stock changes over time.

# Main Workflow
1. Purchase Flow:
Supplier
   ↓
Purchase
   ↓
PurchaseItem
   ↓
Batch
   ↓
Inventory


2. Sales Flow:
Customer
   ↓
Sale
   ↓
SaleItem
   ↓
FEFO Allocation
   ↓
BatchAllocation
   ↓
InventoryMovement


4. Customer Return:
Sale
   ↓
CustomerReturn
   ↓
CustomerReturnItem
   ↓
InventoryMovement


6. Supplier Return:
Purchase
   ↓
SupplierReturn
   ↓
SupplierReturnItem
   ↓
InventoryMovement


# Tech Stack
| Technology             | Purpose                                   |
| ---------------------- | ----------------------------------------- |
| C#                     | Backend development                       |
| .NET Framework Web API | REST API                                  |
| Entity Framework 6     | ORM                                       |
| EF6 Code First         | Database migrations and schema management |
| MySQL                  | Relational database                       |
| MySQL Workbench 8.0 CE | Database management                       |
| JWT                    | Authentication                            |
| PBKDF2 + SHA-256       | Password hashing                          |
| Visual Studio 2026     | Development environment                   |

# Setup
**Prerequisites**
1. Visual Studio
2. .NET Framework development tools
3. Access to a MySQL 8.0 database
4. MySQL Workbench 8.0 CE (optional, for database management)
   
1. Clone the Repository
git clone <repo-url>

2. Open the Solution

Open:

PharmacyManagementSystem.sln

in Visual Studio.

3. Restore NuGet Packages

Restore the required NuGet packages through Visual Studio.

4. Configure Local Application Settings

Sensitive credentials are not committed to the repository.

The project includes safe example configuration files:

Pharma.WebApi/AppSecrets.example.config
Pharma.WebApi/ConnectionStrings.example.config

Create local copies named:

AppSecrets.config
ConnectionStrings.config

inside the Pharma.WebApi project.

JWT Configuration

Copy:

AppSecrets.example.config

to:

AppSecrets.config

and replace the placeholder JWT secret:

<appSettings>
  <add key="JwtSecretKey" value="YOUR_JWT_SECRET_HERE" />
</appSettings>
Database Configuration

Copy:

ConnectionStrings.example.config

to:

ConnectionStrings.config

and replace the placeholder values with your local MySQL credentials:

<connectionStrings>
  <add name="PharmaDbContext"
       connectionString="server=localhost;database=pharmacy_management;uid=YOUR_USERNAME;password=YOUR_PASSWORD;"
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>

The real AppSecrets.config and ConnectionStrings.config files are excluded from source control through .gitignore.

5. Apply Database Migrations

Open the Package Manager Console and run:

Update-Database
6. Run the API

Set:

Pharma.WebApi

as the startup project and run the application.

Security Notes

Sensitive configuration is stored locally and excluded from source control.

Do not commit:

Database credentials
Connection strings containing credentials
JWT secrets
AppSecrets.config
ConnectionStrings.config

Use the provided .example.config files as templates when setting up the project locally.

# Project Status

BPharma is currently a backend-only project.

The system focuses on pharmacy inventory management, secure employee access, batch tracking, FEFO-based stock allocation, sales, purchases, returns, and inventory auditing.





























