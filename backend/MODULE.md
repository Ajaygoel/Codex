# Backend module

## Domain description

The backend module delivers the scheduling and administrative domain for doctor appointments. It models patients, appointment requests, scheduling status, and tenants, and exposes use cases for viewing upcoming appointments, requesting new appointments, and listing tenants.

## API entry points

- `GET /api/appointments`: List upcoming appointments.
- `POST /api/appointments`: Request a new appointment.
- `GET /api/admin/tenants`: List available tenants.

## Primary data models

- `Appointment` (Domain): Tracks scheduled visits, status, and diagnosis summaries.
- `Patient` (Domain): Represents patient identity and demographics.
- `Tenant` (Domain): Represents a tenant/clinic entity.
- `Diagnosis` (Domain): Encapsulates initial diagnosis summary and notes.
