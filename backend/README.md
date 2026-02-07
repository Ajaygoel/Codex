# Backend module

## Purpose

The backend module hosts the .NET API and core application layers for the doctor appointment booking system. It exposes HTTP endpoints, implements application services, and contains the domain model that represents appointments, patients, and tenants.

## Entry points

- `DoctorAppointments.sln`: Solution entry point for the backend.
- `src/Api/Api.csproj`: ASP.NET Core API project entry point (see `src/Api/Program.cs` for composition).
- `src/Application`: Application layer commands/queries and DTOs.
- `src/Domain`: Core domain entities and value objects.
- `src/Infrastructure`: Data access implementations (currently in-memory).
- `tests/Functional.Tests`: Selenium-based functional tests that drive the frontend UI.
