# BPharma


BPharma is a backend Pharmacy Management System built to manage the complete pharmacy workflow; from purchasing items from suppliers to selling them to customers.
The system supports batch-based inventory, purchases, sales, customer returns, supplier returns, employee management, audit tracking, and inventory movements.
BPharma follows the **FEFO** (First Expired, First Out) inventory strategy, where the available batch with the earliest expiry date is prioritized first.



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



# User Roles

BPharma supports three employee roles:

Admin: Full system access and employee management
Pharmacist: Customers, sales, and customer returns
Inventory Manager: Suppliers, purchases, inventory, and supplier returns

Authorization is enforced using role-based access control.



# Authentication and Security

BPharma uses **JWT (JSON Web Token) authentication** to protect API endpoints.
Passwords are never stored in plain text.
Password hashing is implemented using **PBKDF2** through `Rfc2898DeriveBytes` with:

- SHA-256
- 100,000 iterations
- 16-byte random salt
- 32-byte derived hash

Each password receives a unique cryptographically generated salt.

The stored format is:
iterations.salt.hash



# Architecture

BPharma follows a layered architecture:

Client/Api Consumer -> AuthService.Login() ->Pharma.WebApi ->Pharma.BLL ->Pharma.DAL -> Entity Framework EF6 ->MySQL



# Project Structure

PharmacyManagementSystem
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
└── Pharma.WebApi
    ├── App_Start
    ├── Controllers
    ├── DTOs
    ├── Models
    ├── Security
    ├── Global.asax
    └── Web.config

**Layer Responsibilities**

-Pharma.Entity
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
The ERD includes the main relationships between customers, sales, returns, purchases, batches, inventory movements, and supporting item entities.
Supplier, batch, inventory movement, supplier-item, formula, and category relationships are also part of the design.

The link for the ERD:-
[Open Full ERD] docs/BPharma_ERD.svg



# FEFO Inventory Management

BPharma uses First Expired, First Out (FEFO) for stock allocation.
If the same item exists in multiple batches, the batch with the earliest valid expiry date is used first.

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
 |
 v
SaleItem
 |
 v
BatchAllocation
 |
 v
Batch

This preserves batch-level traceability for every sale.


# Inventory Movements

Inventory changes are recorded using InventoryMovement.
Typical movement types include:

-PurchaseIn
-SaleOut
-CustomerReturnIn
-SupplierReturnOut
-AdjustmentIn
-AdjustmentOut

This provides an audit trail of how stock changes over time.



# Main Workflow


1. Purchase Flow

Supplier
   ↓
Purchase
   ↓
PurchaseItem
   ↓
Batch
   ↓
Inventory


2. Sales Flow

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


3. Customer Return

  Sale
   ↓
CustomerReturn
   ↓
CustomerReturnItem
   ↓
InventoryMovement


4. Supplier Return
   
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

Prerequisites

Install:
-Visual Studio
-.NET Framework development tools
-MySQL 8.0
-MySQL Workbench 8.0 CE

1. Clone the Repository
git clone <repository-url>

2. Open the Solution
Open:
PharmacyManagementSystem.sln
in Visual Studio.

3. Restore NuGet Packages
Restore the required NuGet packages through Visual Studio.

4. Configure Database Connection
Sensitive connection strings and credentials should not be committed to GitHub.
Configure your local MySQL connection string before running the application.

Example:

<connectionStrings>
  <add
    name="PharmaDbContext"
    connectionString="server=localhost;database=BPharma;uid=YOUR_USERNAME;pwd=YOUR_PASSWORD;"
    providerName="MySql.Data.MySqlClient" />
</connectionStrings>
    
5. Configure JWT Settings
Configure the JWT signing secret locally.
Do not commit real secrets or credentials to source control.

6. Apply Migrations
Open the Package Manager Console and run:

Update-Database

7. Run the API
Set:

Pharma.WebApi

as the startup project and run the application.



# Security Notes

Do not commit:

-Database passwords
-Connection strings containing credentials
-JWT secrets
-Production configuration files containing sensitive information

Use local configuration or ignored configuration files for sensitive values



# Project Status

BPharma is currently a backend-only project.
The system is focused on pharmacy inventory management, secure employee access, batch tracking, FEFO-based stock allocation, sales, purchases, returns, and inventory auditing.






























