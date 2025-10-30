# Lampshade Project  
ASP.NET Core 5 Razor Pages Application

## How to Run

1. Clone the repository:
```bash
git clone https://github.com/hamidbeigi/Lampshade.git
```

2. Create configuration file:
```plaintext
Copy:   appsettings.Development-example.json
Rename: appsettings.Development.json
Update: database connection string and other required settings
```

---

## Database Setup

The project requires a SQL Server database.
We provide a ready-to-use SQL script that creates the schema
and inserts all required default data (including admin user and roles).

Steps:
```plaintext
1. Open SQL Server Management Studio (SSMS)
2. Create a new empty database named Lampshade
3. Open the script file located at: DatabaseScripts/InitDatabase.sql
4. Execute the script
```

### Note
This script ensures that all required default data (such as the admin account and roles) are created correctly, so there is no need to run EF Core migrations manually.

---

## Run the Project

Open the solution in Visual Studio 2019/2022 and press F5 to run the project. The application will start at:
```plaintext
https://localhost:5001/
```

### Administration Panel
```plaintext
URL: https://localhost:5001/administration
Username: hamidbeigi
Password: 24512451
```

## Prerequisites

- SQL Server 2016 or newer
- SQL Server Management Studio (SSMS)
- .NET 5 SDK
- Visual Studio 2019/2022 (recommended)

---

## Summary

```plaintext
Clone → Configure → Run SQL Script → Start the project
```
No manual data entry or migrations required.
