# Flight Reservation System

## Overview

A web-based Flight Reservation System built with ASP.NET Core 8.0 MVC and Entity Framework Core 9.0. The system allows managing flights, passengers, and flight-passenger bookings through a clean CRUD interface. It also includes user authentication with separate User and Admin roles.

## Tech Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 9.0 with SQL Server (Code-First migrations)
- **Views**: Razor Views (.cshtml) with Bootstrap 4
- **Auth**: Custom session-based login (User and Admin tables)
- **Database**: SQL Server (LocalDB for development)

## Project Structure

```
FlightReservationApp_f/
├── Controllers/          # MVC controllers (Flights, Passengers, FlightPassengers, User, Home)
├── Data/                 # EF Core DataContext
├── Models/               # Entity models and ViewModels
├── Views/                # Razor views organized by controller
│   ├── Flights/
│   ├── Passengers/
│   ├── FlightPassengers/
│   ├── User/
│   ├── Home/
│   └── Shared/
├── Migrations/           # EF Core database migrations
└── wwwroot/              # Static assets (CSS, JS, libs)
```

## Core Features

- **Flight Management**: Full CRUD for flights with search by flight number and column sorting (FlightNumber, Origin, Destination, Price)
- **Passenger Management**: Full CRUD for passenger records (name, email, contact)
- **Booking (FlightPassengers)**: Link passengers to flights via a many-to-many junction table; flight details page shows all booked passengers and total cost
- **User Auth**: Sign up and login; login checks both User and Admin tables for role-based redirect
- **Flight Types**: Domestic and International enum

## Data Models

| Model | Key Fields |
|---|---|
| Flight | Id, FlightNumber, Origin, Destination, DepartureTime, ArrivalTime, SeatCapacity, Type (enum), DepartureLocation, Price |
| Passenger | PassengerId, FullName, Email, ContactNumber |
| FlightPassenger | Id, FlightId (FK), PassengerId (FK) |
| User | Id, Name, username (unique), password |
| Admin | AdminId, Name, username (unique), password |

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server or SQL Server LocalDB

### Setup

1. Clone the repository
2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DataContext": "Server=(localdb)\\mssqllocaldb;Database=FlightReservationDB;Trusted_Connection=True;"
   }
   ```
3. Apply migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

The app starts at `https://localhost:PORT/Flights` by default.

## Migrations History

| Migration | Description |
|---|---|
| 20250114021030_InitialCreate | Initial Flight, Passenger, FlightPassenger tables |
| 20250114022923_RevertInitialCreate | Revert placeholder |
| 20250114040821_InitialCreate2 | Recreate tables with correct schema |
| 20250114094633_UpdateFlightModel | Flight model updates |
| 20250114192906_AddPriceToFlight | Add Price column |
| 20250114200937_UpdatePriceColumnType | Change Price to decimal(18,2) |
| 20250115021739_CreateUserTable | Add User and Admin tables |
