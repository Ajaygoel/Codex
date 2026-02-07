# Doctor Appointment Booking Base Project

This repository provides a starter Angular + .NET solution for a doctor appointment booking system with support for recording an initial patient diagnosis, multi-tenant routing, and SSO-ready authentication headers. The structure follows SOLID principles and separation of concerns using a clean architecture layout.

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

## Build steps

### Backend

```bash
cd backend
# dotnet restore
# dotnet build
```

### Frontend

```bash
cd frontend
# npm install
# npm run build
```

## Run instructions

### Backend

```bash
cd backend
# dotnet run --project src/Api/Api.csproj
```

### Frontend

```bash
cd frontend
# npm start
```

> The current defaults assume the API runs on `http://localhost:5000`.

## Code conventions

- Keep API controllers thin: delegate work to application services and keep endpoints focused on HTTP concerns.
- Model core business logic in the Domain layer; avoid leaking persistence or transport types into Domain.
- Use dependency injection for application services and repository abstractions.
- Prefer explicit DTOs at the application boundary (API <-> Application, Application <-> Frontend).
- Name components/services with their feature intent (e.g., `AppointmentService`, `AdminPortalComponent`).

## Backend overview (.NET)

- **Domain**: Entities (`Appointment`, `Patient`) and value objects (`Diagnosis`) live here.
- **Application**: Use cases and DTOs live here (interfaces for repositories and services).
- **Infrastructure**: Implements data access (currently in-memory for local development).
- **API**: Thin controllers that call application services and enforce SSO headers.

## Frontend overview (Angular)

- **Models**: Shared TypeScript types for appointments.
- **Services**: `AppointmentService` encapsulates API interaction.
- **Components**: `AppComponent` includes a booking form, tenant/user context controls, a doctor dashboard, and an admin portal.

## Multi-tenant & SSO headers

The API expects the following headers for each request:

- `X-Tenant-Id` (tenant scope; defaults to `north-clinic` if omitted)
- `X-User-Id` (SSO user identifier; required)
- `X-User-Name` (display name)
- `X-User-Roles` (comma-separated roles)

The Angular app uses a simple interceptor to send these headers based on the context form.

## Next steps

- Swap `InMemoryAppointmentRepository` with a database-backed implementation.
- Add validation and error handling policies.
- Add authentication and authorization.
- Expand domain behavior (e.g., availability, clinician schedules).
