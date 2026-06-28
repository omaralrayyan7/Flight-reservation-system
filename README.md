# Flight Reservation System

## Overview
A web-based Flight Reservation System built with ASP.NET Core 8.0 MVC and Entity Framework Core 9.0. Manages flights, passengers, and bookings through a full CRUD interface with user authentication (User and Admin roles).

## Tech Stack
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 9.0.1 with SQL Server (Code-First migrations)
- **Views**: Razor Views (.cshtml) with Bootstrap 4
- **Auth**: Custom login checking User and Admin tables
- **Database**: SQL Server / LocalDB

## Project Structure
```
FlightReservationApp_f/
├── Controllers/          # MVC controllers
├── Data/                 # EF Core DataContext
├── Models/               # Entity models
├── Views/                # Razor views per controller
├── Migrations/           # EF Core database migrations
└── wwwroot/              # Static assets
```

## Core Features
- **Flight Management**: CRUD with search by flight number and sorting by FlightNumber, Origin, Destination, Price
- **Passenger Management**: CRUD for passenger records
- **Bookings**: Many-to-many Flight ↔ Passenger via FlightPassenger table; Details view shows all passengers and total cost
- **User Auth**: Sign up / login; supports both User and Admin accounts
- **Flight Types**: Domestic and International (enum)

## Data Models

| Model | Key Fields |
|---|---|
| Flight | Id, FlightNumber, Origin, Destination, DepartureTime, ArrivalTime, SeatCapacity, Type (enum), DepartureLocation, Price (decimal) |
| Passenger | PassengerId, FullName, Email, ContactNumber |
| FlightPassenger | Id, FlightId (FK), PassengerId (FK) |
| User | Id, Name, username (unique), password |
| Admin | AdminId, Name, username (unique), password |

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server or SQL Server LocalDB

### Setup
1. Clone the repo
2. Update the connection string in `appsettings.json`
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```
4. Run:
   ```bash
   dotnet run
   ```

The app opens at `https://localhost:PORT/Flights` by default.
