# Vehicle Renting Microservice – GT Motive Technical Test

## Introduction
This project implements a microservice for managing a vehicle renting fleet, following **Hexagonal Architecture** and **Clean Architecture** principles. It allows managing vehicles and processing rentals with specific business rules.

## Business Rules
- A customer **cannot rent more than one vehicle** at the same time.
- Vehicles **older than 5 years** cannot be added to the fleet.
- Only **available vehicles** can be rented and listed.

## Architecture
The solution follows a strict Hexagonal Architecture approach:

- **Domain**: Contains entities and business logic. No dependencies.
- **Application**: Implements use cases and defines ports.
- **Infrastructure**: Provides adapters (Repositories) and external integrations.
- **API**: REST controllers acting as the entry point.

The application core is completely isolated from infrastructure and framework dependencies.

## Technologies
- **.NET 9**
- **ASP.NET Core Web API**
- **MediatR**
- **Persistence**: Hybrid support for **In-Memory** and **MongoDB**.
- **Docker & Docker Compose**
- **xUnit** (Unit, Integration & Functional tests)

## API Documentation
Once the application is running, you can explore and test the endpoints using Swagger UI:

👉 **[http://localhost:8080/swagger](http://localhost:8080/swagger)**

### Main Endpoints
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/vehicles` | Adds a new vehicle to the fleet (validating manufacturing date). |
| `GET` | `/api/vehicles/available` | Retrieves a list of all vehicles currently available. |
| `POST` | `/api/vehicles/rent` | Rents a vehicle for a specific customer. |
| `POST` | `/api/vehicles/return` | Returns a rented vehicle and makes it available again. |

## Configuration (Persistence)
The microservice supports two persistence modes controlled by the `UseMongoDb` flag in `appsettings.json` or Environment Variables.

### 1. In-Memory (Default for dev)
Fast and simple, no external dependencies required.

### 2. MongoDB
To use MongoDB, ensure a Mongo instance is running and update the configuration:
- **Variable**: `AppSettings__UseMongoDb=true`
- **Connection String**: `MongoDb__ConnectionString`

*Note: The Docker Compose configuration is already set up to use MongoDB by default.*

## Run Locally

### 1. Manual Execution (.NET CLI)
To run the application manually without Docker using the In-Memory database:

1.  **Change Persistence Flag**: Open `src/GtMotive.Estimate.Microservice.Host/appsettings.json` and set the `UseMongoDb` flag to **`false`**:
    ```json
    "AppSettings": {
      "UseMongoDb": false
    }
    ```
2.  **Run from Root**: Open a terminal in the project root folder and execute:
    ```bash
    dotnet run --project src/GtMotive.Estimate.Microservice.Host
    ```
3.  **Run test from src**: Open a terminal in the project root folder and execute:3
    ```bash
    dotnet test --project src
    ```

### 2. Using Docker (Recommended)
This will spin up the API and a MongoDB container automatically.

```bash
# From the project root folder
docker-compose up --build -d