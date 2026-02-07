# Doctor Appointment Booking Base Project

This repository provides a starter Angular + .NET solution for a doctor appointment booking system with support for recording an initial patient diagnosis. The structure follows SOLID principles and separation of concerns using a clean architecture layout.

## Repository layout

```
backend/
  DoctorAppointments.sln
  src/
    Api/            # HTTP API layer
    Application/    # Use cases, service interfaces, DTOs
    Domain/         # Core entities and value objects
    Infrastructure/ # Data access implementations
frontend/
  src/             # Angular application
```

## Backend overview (.NET)

- **Domain**: Entities (`Appointment`, `Patient`) and value objects (`Diagnosis`) live here.
- **Application**: Use cases and DTOs live here (interfaces for repositories and services).
- **Infrastructure**: Implements data access (currently in-memory for local development).
- **API**: Thin controllers that call application services.

## Frontend overview (Angular)

- **Models**: Shared TypeScript types for appointments.
- **Services**: `AppointmentService` encapsulates API interaction.
- **Components**: `AppComponent` includes a booking form and list of upcoming appointments.

## Running locally (requires tooling installed)

### Backend

```bash
cd backend
# dotnet restore
# dotnet run --project src/Api/Api.csproj
```

### Frontend

```bash
cd frontend
# npm install
# npm start
```

> The current defaults assume the API runs on `http://localhost:5000`.

## Next steps

- Swap `InMemoryAppointmentRepository` with a database-backed implementation.
- Add validation and error handling policies.
- Add authentication and authorization.
- Expand domain behavior (e.g., availability, clinician schedules).
