# ItemTrackerAPI

ItemTrackerAPI is a simple RESTful API built with **ASP.NET Core Web API** and **C#** for managing items in a data store.  
It’s intended as a clean reference / starter project for CRUD-style APIs using Entity Framework Core, DTOs, and a layered design.

---

## 🚀 Features

- ✅ Create new items  
- ✅ Read all items or a single item by ID  
- ✅ Update existing items  
- ✅ Delete items  
- ✅ Uses DTOs and AutoMapper for clean separation between entities and API contracts  
- ✅ Backed by Entity Framework Core with code-first migrations

---

## 🧱 Tech Stack

- **Framework:** ASP.NET Core Web API (.NET)  
- **Language:** C#  
- **ORM:** Entity Framework Core (code-first, Migrations folder present)  
- **Mapping:** AutoMapper (via `ItemProfile`)  
- **Database:** Configurable via `appsettings.json` (e.g. SQL Server / local DB)

---

## 📁 Project Structure

```text
ItemTrackerAPI/
├── Controllers/
│   └── ItemsController.cs        # API endpoints for items
├── DTOs/                         # Data transfer objects for requests/responses
├── Entities/                     # Domain / persistence entities
├── Migrations/                   # EF Core migrations
├── ItemDbContext.cs              # EF Core DbContext
├── ItemProfile.cs                # AutoMapper profile
├── Program.cs                    # App startup & configuration
├── appsettings.json              # Configuration & connection string(s)
└── ItemTrackerAPI.csproj
