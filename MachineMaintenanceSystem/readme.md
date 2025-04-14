# Machine Maintenance System API

The **Machine Maintenance System API** is a .NET 9-based web application designed to manage maintenance records, equipment, and related data for a machine maintenance system. It provides endpoints for managing maintenance schedules, equipment, technicians, and part usage.

## Features

- **Maintenance Records**: Track maintenance activities, including emergency and scheduled maintenance.
- **Equipment Management**: Manage equipment details, including installation dates, manufacturers, and maintenance history.
- **Part Usage**: Record parts used during maintenance.
- **Technician Management**: Assign technicians to maintenance tasks.
- **OpenAPI Integration**: Automatically generated API documentation using OpenAPI.

## Technologies Used

- **.NET 9**: Modern, high-performance framework for building web APIs.
- **Entity Framework Core**: For database access and management.
- **PostgreSQL**: Database provider for storing application data.
- **OpenAPI**: For API documentation and testing.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL database
- Visual Studio 2022 or any compatible IDE

## Getting Started

### 1. Clone the Repository
### 2. Configure the Database

Update the connection string in `appsettings.json` to point to your PostgreSQL database.
### 3. Apply Migrations

Run the following commands to apply database migrations:
### 4. Run the Application

Start the application using the following command:
The API will be available at `https://localhost:5001` (or the configured port).

### 5. Access API Documentation

Open the OpenAPI documentation in your browser at:
## Project Structure

- **`Models/`**: Contains entity models such as `MaintenanceRecord`, `Equipment`, and `PartUsage`.
- **`Program.cs`**: Entry point of the application.
- **`appsettings.json`**: Configuration file for database and other settings.

## Example Endpoints
