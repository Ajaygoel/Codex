# Frontend module

## Domain description

The frontend module provides the clinician-facing workflow for requesting appointments, reviewing upcoming schedules, and administering tenant context. It manages user input for patient details and diagnosis and sends requests to the backend API with tenant-aware headers.

## API entry points

The Angular app calls the backend API endpoints:

- `GET /api/appointments`
- `POST /api/appointments`
- `GET /api/admin/tenants`

## Primary data models

- `AppointmentSummary`: Displays booked appointment details in lists and dashboards.
- `AppointmentRequest`: Payload for creating a new appointment request.
- `PatientInput`: Patient identity details for appointment requests.
- `DiagnosisInput`: Optional initial diagnosis details.
- `Tenant`: Tenant metadata for admin views.
